using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ContractMonthlyClaimSystem.Service;
using AspNetCoreGeneratedDocument;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Data;
using System;

namespace ContractMonthlyClaimSystem.Controllers
{

    public class HomeController : Controller
    {
        private readonly claimTBL _claimTBL;
        private readonly LoginModel _loginModel;
        private readonly cmcs_userTBL _usertbl;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, claimTBL claimTBL, AppDbContext context, cmcs_userTBL usertbl)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
            _claimTBL = claimTBL ?? throw new ArgumentException(nameof(claimTBL));
            _loginModel = new LoginModel(context);
            _usertbl = usertbl;
        }

        public async Task<IActionResult> Index()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            var claims = await _claimTBL.GetClaimsAsync();
            string accountType = _loginModel.GetAccountType(userID);
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            ViewData["accountType"] = accountType;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Claim()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            var claims = await _claimTBL.GetClaimsAsync();
            string accountType = _loginModel.GetAccountType(userID);
            ViewData["Claims"] = claims;
            ViewData["UserID"] = userID;
            ViewData["accountType"] = accountType;

            return View("Claim", "Home");
        }

        public IActionResult insertClaim()
        {
            return View();
        }

        public IActionResult Login()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            string accountType = _loginModel.GetAccountType(userID);
            ViewData["UserID"] = userID;
            ViewData["accountType"] = accountType;
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult EditProfile()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            ViewData["UserID"] = userID;
            return View();
        }
        public IActionResult ProfileUpdated()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            ViewData["UserID"] = userID;
            return View();
        }
    }

}
