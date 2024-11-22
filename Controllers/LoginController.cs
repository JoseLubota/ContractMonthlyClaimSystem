using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ContractMonthlyClaimSystem.Data;
using System;

namespace ContractMonthlyClaimSystem.Controllers
{


    public class LoginController : Controller
    {
        private readonly LoginModel _loginModel;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _loginModel = new LoginModel(context);
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                int userID = _loginModel.SelectUser(email, password);
                if (userID != -1)
                {
                    //Store user ID section
                    HttpContext.Session.SetInt32("userID", userID);
                    //
                    // return RedirectToAction("Index", "Home", new { userID = userID });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //
                    return View("Error");
                }

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Login failed for {EMAIL}", email);
                ModelState.AddModelError("", "An error occured while attempting to log in");
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("userID");
            return RedirectToAction("Login", "Home");

        }
        public IActionResult Privacy()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            if (userID == null)
            {
                ViewData["userID"] = 0;
                ViewData[""] = string.Empty;
            }
            else
            {
                ViewData["userID"] = userID;

            }

            return View();
        }



    }

}
