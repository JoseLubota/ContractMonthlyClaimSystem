using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static Azure.Core.HttpHeader;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimModel
    {

        [Key]
        public int? CLAIM_ID { get; set; }
        public string STATUS { get; set; }
        public int? APPROVER_ID { get; set; }
        public int LECTURER_ID { get; set; }
        public string NOTES { get; set; }
        public string? HOURS_WORKED { get; set; }
        public string? HOURLY_RATE { get; set; }
        //  public string EMAIL { get; set; }
        //   public string PHONE_NUMBER { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        //   public string ACCOUNT_TYPE { get; set; }


    }

}





