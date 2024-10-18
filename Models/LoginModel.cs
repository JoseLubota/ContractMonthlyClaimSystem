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
                string sql = "SELECT USERID FROM cmcs_userTBL WHERE PASSWORD = @PASSWORD and  EMAIL = @EMAIL";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userID = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new InvalidOperationException("Login Failed: Invalid email or password");

                    }
                } 
                catch (Exception ex)
                {
                    throw new InvalidOperationException("An error occured while attempting to log in");
                }
            }
            return userID;
        }
        public string GetAccountType(int? USERID)
        {
            if(USERID == null)
            {
                USERID = 0;
            }
            string accountType = string.Empty;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string sql = "SELECT ACCOUNT_TYPE FROM dbo.cmcs_userTBL WHERE USERID = @USERID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        accountType = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return accountType;

        }

        public static SqlConnection con = new SqlConnection(conString);
    }
}
