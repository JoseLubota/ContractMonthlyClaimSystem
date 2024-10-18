using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;


namespace ContractMonthlyClaimSystem.Service
{
   
    public class FileService
    {
        private readonly string _connectionString;
        private readonly string _shareName;
        private readonly ShareServiceClient _shareServiceClient;
        private readonly IConfiguration _configuration;


        public FileService(IConfiguration configuration)
        {
            _connectionString = configuration["AzureStorage:ConnectionString"];
            _shareName = configuration["AzureStorage:ShareName"];
            _configuration = configuration;
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException(nameof(_connectionString), "The connection is not set in the configuration");
            }
        }
        public async Task UploadFileAsync(string filePath, string fileName)
        {
            ShareClient share = new ShareClient(_connectionString, _shareName);
            ShareDirectoryClient directory = share.GetDirectoryClient("Service");
            await directory.CreateIfNotExistsAsync();

            ShareFileClient file = directory.GetFileClient(fileName);

            using(FileStream stream = File.OpenRead(filePath))
            {
                await file.CreateAsync(stream.Length);
                await file.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
            }
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            ShareClient share = new ShareClient(_connectionString, _shareName);
            ShareDirectoryClient directory = share.GetDirectoryClient("Service");
            ShareFileClient file = directory.GetFileClient(fileName);

            ShareFileDownloadInfo download = await file.DownloadAsync();
            return download.Content;
        }

    }
}
