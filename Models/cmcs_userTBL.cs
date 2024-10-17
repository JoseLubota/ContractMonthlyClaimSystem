using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class cmcs_userTBL
    {
        public static string conString = "Server=tcp:clvd-sql-server.database.windows.net,1433;Initial Catalog=clvd-db;Persist Security Info=False;User ID=Jose;Password=2004Fr@ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlConnection con = new SqlConnection(conString);

        public int USERID { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string PASSWORD { get; set; }


        public int insert_User(cmcs_userTBL u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string sql = "INSERT INTO cmcs_userTBL (STATUS, APPROVER_ID, LECTURER_ID,  NOTES, HOURS_WORKED, HOURLY_RATE) VALUES(@STATUS, @APPROVER_ID, @LECTURER_ID, @NOTES , @HOURS_WORKED, @HOURLY_RATE)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@STATUS", u.FULL_NAME);
                    cmd.Parameters.AddWithValue("@APPROVER_ID", u.EMAIL);
                    cmd.Parameters.AddWithValue("@LECTURER_ID", u.PHONE_NUMBER);
                    cmd.Parameters.AddWithValue("@NOTES", u.ADDRESS);
                    cmd.Parameters.AddWithValue("@HOURS_WORKED", u.ACCOUNT_TYPE);
                    cmd.Parameters.AddWithValue("@HOURLY_RATE", u.PASSWORD);
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
