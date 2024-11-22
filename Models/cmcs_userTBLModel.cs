using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class cmcs_userTBLModel
    {
        [Key]
        public int USERID { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string PASSWORD { get; set; }
    }
}
