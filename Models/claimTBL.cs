using ContractMonthlyClaimSystem.Controllers;
using System.Data.SqlClient;

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

        public int insert_Claim(claimTBL c, int? userID)
        {
            try
            {
                int lecturerID = Convert.ToInt32(userID);
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string sql = "INSERT INTO claimTBL (LECTURER_ID,  NOTES, HOURS_WORKED, HOURLY_RATE) VALUES( @LECTURER_ID, @NOTES , @HOURS_WORKED, @HOURLY_RATE)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@LECTURER_ID", lecturerID);
                    cmd.Parameters.AddWithValue("@NOTES", c.NOTES);
                    cmd.Parameters.AddWithValue("@HOURS_WORKED", c.HOURS_WORKED);
                    cmd.Parameters.AddWithValue("@HOURLY_RATE", c.HOURLY_RATE);
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
