using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class LoginModel : Controller
    {
        public static string conString = "Server=tcp:clvd-sql-server.database.windows.net,1433;Initial Catalog=clvd-db;Persist Security Info=False;User ID=Jose;Password=2004Fr@ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public int SelectUser(string EMAIL, string PASSWORD)
        {
            int userID = -1;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string sql = "SELECT USERID FROM cmcs_userTBL WHERE EMAIL = @EMAIL and PASSWORD = @PASSWORD";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return userID;
        }

        public static SqlConnection con = new SqlConnection(conString);
    }
}
