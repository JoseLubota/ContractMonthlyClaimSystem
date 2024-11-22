using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Service
{
    public interface IUserService
    {
        cmcs_userTBLModel GetUserById(int userId);
        void UpdateUser(cmcs_userTBLModel user);
        //  Task<int?> GetLastUserIDAsync();
        //  Task<int?> Insert_User(cmcs_userTBL user);

    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public cmcs_userTBLModel GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void UpdateUser(cmcs_userTBLModel user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }



    }
}
