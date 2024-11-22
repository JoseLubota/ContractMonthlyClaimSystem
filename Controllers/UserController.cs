using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContractMonthlyClaimSystem.Controllers
{

    public class UserController : Controller
    {

        private readonly cmcs_userTBL _usertbl;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;


        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, cmcs_userTBL usertbl)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _usertbl = usertbl ?? throw new ArgumentException(nameof(usertbl)); ;
            //  _context = context;
        }
        /*
        [HttpGet]
        public IActionResult EditProfile()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            ViewData["UserID"] = userID;
            return View();
        }
        */
        [HttpPost]
        public IActionResult EditProfile(cmcs_userTBLModel user)
        {
            if (ModelState.IsValid)
            {
                int userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetInt32("userID"));
                var existingUser = _userService.GetUserById(userID);
                existingUser.USERID = userID;
                existingUser.FULL_NAME = user.FULL_NAME;
                existingUser.EMAIL = user.EMAIL;
                existingUser.PASSWORD = user.PASSWORD;
                existingUser.ADDRESS = user.ADDRESS;
                existingUser.ACCOUNT_TYPE = user.ACCOUNT_TYPE;
                _userService.UpdateUser(existingUser);
                return RedirectToAction("ProfileUpdated", "Home");
            }
            return View();
        }

        public IActionResult ProfileUpdated()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SignUp(cmcs_userTBL Users)
        {
            var result = await _usertbl.Insert_User(Users);
            if (result != null)
            {
                return RedirectToAction("Claim", "Home");
            }
            return View("error");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }



    }

}
