﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static Azure.Core.HttpHeader;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimModel : Controller
    {
        public int CLAIM_ID { get; set; }
        public string STATUS { get; set; }
        public int? APPROVER_ID { get; set; }
        public int LECTURER_ID { get; set; }
        public string NOTES { get; set; }
        public string HOURS_WORKED { get; set; }
        public string HOURLY_RATE { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public ClaimModel() { }
        public ClaimModel(int cLAIM_ID, string sTATUS, int aPPROVER_ID, int lECTURER_ID, string nOTES, string hOURS_WORKED, string hOURLY_RATE, string eMAIL, string pHONE_NUMBER,string documentName, DateTime date)
        {
            CLAIM_ID = cLAIM_ID;
            STATUS = sTATUS;
            APPROVER_ID = aPPROVER_ID;
            LECTURER_ID = lECTURER_ID;
            NOTES = nOTES;
            HOURS_WORKED = hOURS_WORKED;
            HOURLY_RATE = hOURLY_RATE;
            EMAIL = eMAIL;
            PHONE_NUMBER = pHONE_NUMBER;
            DOCUMENT_NAME = documentName;
            TIMESTAMP = date;
        }
        public static List<ClaimModel> SelectClaims()
        {
            List<ClaimModel> claims = new List<ClaimModel>();
            string con_string = "Server=tcp:clvd-sql-server.database.windows.net,1433;Initial Catalog=clvd-db;Persist Security Info=False;User ID=Jose;Password=2004Fr@ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT us.ACCOUNT_TYPE, cl.TIMESTAMP , cl.DOCUMENT_NAME , cl.CLAIM_ID, cl.STATUS, cl.HOURS_WORKED, cl.HOURLY_RATE, cl.NOTES, cl.LECTURER_ID, us.USERID, us.ACCOUNT_TYPE, us.EMAIL, cl.APPROVER_ID FROM dbo.claimTBL cl JOIN dbo.cmcs_userTBL us ON us.USERID = cl.LECTURER_ID;";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClaimModel claim = new ClaimModel();
                    claim.CLAIM_ID = Convert.ToInt32(reader["CLAIM_ID"]);
                    claim.STATUS = Convert.ToString(reader["STATUS"]);
                    claim.APPROVER_ID = reader["APPROVER_ID"] != DBNull.Value ? Convert.ToInt32(reader["APPROVER_ID"]) : (int?)null;
                    claim.LECTURER_ID = Convert.ToInt32(reader["LECTURER_ID"]);
                    claim.NOTES = Convert.ToString(reader["NOTES"]);
                    claim.HOURS_WORKED = Convert.ToString(reader["HOURS_WORKED"]);
                    claim.HOURLY_RATE = Convert.ToString(reader["HOURLY_RATE"]);
                    claim.EMAIL = Convert.ToString(reader["EMAIL"]);
                    claim.DOCUMENT_NAME = Convert.ToString(reader["DOCUMENT_NAME"]);
                    claim.ACCOUNT_TYPE = Convert.ToString(reader["ACCOUNT_TYPE"]);
                    claim.TIMESTAMP = Convert.ToDateTime(reader["TIMESTAMP"]);
                    claims.Add(claim);
                }
                reader.Close();
            }
            return claims;
        }
        public static List<ClaimModel> updatedClaims(int CLAIM_ID, string STATUS, int APPROVER_ID)
        {
            List<ClaimModel> claims = new List<ClaimModel>();
            string con_string = "Server=tcp:clvd-sql-server.database.windows.net,1433;Initial Catalog=clvd-db;Persist Security Info=False;User ID=Jose;Password=2004Fr@ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            string updateQuery = "UPDATE dbo.claimTBL SET STATUS = @STATUS, APPROVER_ID = @APPROVER_ID WHERE CLAIM_ID = @CLAIM_ID";

            using (SqlConnection con = new SqlConnection(con_string))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@CLAIM_ID", CLAIM_ID);
                    cmd.Parameters.AddWithValue("@APPROVER_ID", APPROVER_ID);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) 
                    {
                        claims = ClaimModel.SelectClaims();
                    }
                }
            }
            return claims;
        }
        
    }
}
