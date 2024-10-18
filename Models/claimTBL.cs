using ContractMonthlyClaimSystem.Controllers;
using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Service;

namespace ContractMonthlyClaimSystem.Models
{
    public class claimTBL
    {
        public static string conString = "Server=tcp:clvd-sql-server.database.windows.net,1433;Initial Catalog=clvd-db;Persist Security Info=False;User ID=Jose;Password=2004Fr@ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static SqlConnection con = new SqlConnection(conString);
       

        public int USERID { get; set; }
        public string STATUS { get; set; }
        public string APPROVER_ID { get; set; }
        public string LECTURER_ID { get; set; }
        public string NOTES { get; set; }
        public string HOURS_WORKED { get; set; }
        public string HOURLY_RATE { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; } = DateTime.Now;

        public async Task<int> insert_Claim(claimTBL c, int? userID, IFormFile document, FileService fileService)
        {
            try
            {
                int lecturerID = Convert.ToInt32(userID);
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string fileName = null;
                    TIMESTAMP = DateTime.Now;

                    if (document != null && document.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await document.CopyToAsync(stream);
                        }
                        fileName = "Claim_" + lecturerID + "_" + DateTime.Now.Ticks + "_" + document.FileName;
                        await fileService.UploadFileAsync(filePath, fileName);
                    }
                    string documentName = fileName ?? string.Empty;
                    c.TIMESTAMP = DateTime.Now;
                    string sql = "INSERT INTO claimTBL (LECTURER_ID,  NOTES, HOURS_WORKED, HOURLY_RATE, STATUS,DOCUMENT_NAME,TIMESTAMP ) VALUES( @LECTURER_ID, @NOTES , @HOURS_WORKED, @HOURLY_RATE, @STATUS, @DOCUMENT_NAME, @TIMESTAMP)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@LECTURER_ID", lecturerID);
                    cmd.Parameters.AddWithValue("@NOTES", c.NOTES);
                    cmd.Parameters.AddWithValue("@HOURS_WORKED", c.HOURS_WORKED);
                    cmd.Parameters.AddWithValue("@HOURLY_RATE", c.HOURLY_RATE);
                    cmd.Parameters.AddWithValue("@STATUS", "Pending");
                    cmd.Parameters.AddWithValue("@DOCUMENT_NAME",documentName);
                    cmd.Parameters.AddWithValue("@TIMESTAMP", c.TIMESTAMP);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
