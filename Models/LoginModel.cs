using ContractMonthlyClaimSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class LoginModel : Controller
    {
        private readonly AppDbContext _context;
        public LoginModel()
        {
        }
        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        public int SelectUser(string EMAIL, string PASSWORD)
        {

            try
            {
                var user = _context.Users
                    .Where(u => u.EMAIL.ToLower() == EMAIL.ToLower() && u.PASSWORD.ToLower() == PASSWORD.ToLower())
                    .Select(u => u.USERID)
                    .FirstOrDefault();

                if (user != 0)
                {
                    return user;
                }
                else
                {
                    throw new InvalidOperationException("Login Failed: Invalid email or password");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new InvalidOperationException("An error occured while attempting to log in");
            }

        }
        public string GetAccountType(int? USERID)
        {
            {
                if (_context == null)
                {
                    return "Not found";
                }
                try
                {
                    var accountType = _context.Users
                     .Where(u => u.USERID == USERID)
                     .Select(u => u.ACCOUNT_TYPE)
                     .FirstOrDefault();

                    return accountType ?? string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

    }
}
