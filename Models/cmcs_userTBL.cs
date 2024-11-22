using System.Data.SqlClient;
using System.Security.Claims;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Models
{
    public class cmcs_userTBL
    {
        private readonly AppDbContext _context;

        public cmcs_userTBL(AppDbContext context)
        {
            _context = context;
        }
        public cmcs_userTBL() { }

        public int USERID { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string PASSWORD { get; set; }

        public async Task<int?> GetLastUserIDAsync()
        {
            var lastUser = await _context.Users
                .OrderByDescending(u => u.USERID)
                .FirstOrDefaultAsync();

            return lastUser == null ? 1 : lastUser.USERID + 1;

        }
        public async Task<int?> Insert_User(cmcs_userTBL u)
        {

            try
            {

                int? userID = await GetLastUserIDAsync();
                u.USERID = Convert.ToInt32(userID);

                var user = new cmcs_userTBLModel
                {
                    USERID = u.USERID,
                    FULL_NAME = u.FULL_NAME,
                    EMAIL = u.EMAIL,
                    PHONE_NUMBER = u.PHONE_NUMBER,
                    ADDRESS = u.ADDRESS,
                    ACCOUNT_TYPE = u.ACCOUNT_TYPE,
                    PASSWORD = u.PASSWORD,
                };
                _context.Users.Add(user);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
