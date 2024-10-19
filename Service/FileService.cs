using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Azure;


namespace ContractMonthlyClaimSystem.Service
{
   
    public class FileService
    {
        private readonly string _connectionString;
        private readonly string _shareName;
        private readonly ShareServiceClient _shareServiceClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileService> _logger;
        private IConfiguration configuration;

        public FileService(IConfiguration configuration, ILogger<FileService> logger)
        {
            _connectionString = configuration["AzureStorage:ConnectionString"];
            _shareName = configuration["AzureStorage:ShareName"];
            _configuration = configuration;
            _logger = logger;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException(nameof(_connectionString), "The connection is not set in the configuration");
            }
        }

        public FileService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task UploadFileAsync(string filePath, string fileName)
        {
            try
            {
                ShareClient share = new ShareClient(_connectionString, _shareName);
                ShareDirectoryClient directory = share.GetDirectoryClient("Service");
                await directory.CreateIfNotExistsAsync();

                ShareFileClient file = directory.GetFileClient(fileName);

                using (FileStream stream = File.OpenRead(filePath))
                {
                    await file.CreateAsync(stream.Length);
                    await file.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
                }

            }catch(RequestFailedException ex)
            {
                _logger.LogError(ex, "Failed to upload '{FileName}' to Azure Storage. Status: {Status}, Error Code:  {ErrorCode}",fileName, ex.Status, ex.ErrorCode);
                throw new InvalidOperationException("An Error occured while uploading the file to Azure Storage.", ex);

            }catch(FileNotFoundException ex)
            {
                _logger.LogError(ex, "File not found: {filePath}", filePath);
                throw new FileNotFoundException("The specified file does not exist", ex);

            }catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured while upload the file '{FileName}'.", fileName);
                throw new InvalidOperationException("An unexpected error occured while upload the file", ex);
            }

        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            try
            {
                ShareClient share = new ShareClient(_connectionString, _shareName);
                ShareDirectoryClient directory = share.GetDirectoryClient("Service");
                ShareFileClient file = directory.GetFileClient(fileName);

                ShareFileDownloadInfo download = await file.DownloadAsync();
                return download.Content;

            }
            catch (RequestFailedException ex) when (ex.ErrorCode == ShareErrorCode.ResourceNotFound)
            {
                _logger.LogError(ex,"File '{fileName}' not dounf in Azure Storage.", fileName);
                throw new FileNotFoundException("The specified file does not exist in Azure Storage");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An unexpected error occured while download the file '{fileName}'.", fileName);
                throw new InvalidOperationException("An unexpected error occured while upload the file", ex);
            }
        }


    }
}
