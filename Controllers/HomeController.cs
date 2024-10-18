using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            List<ClaimModel> claims;
            claims = ClaimModel.SelectClaims();
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            ViewData["accountType"] = accountType;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Claim()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            List<ClaimModel> claims;
            claims = ClaimModel.SelectClaims();
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
            ViewData["Claims"] = claims;
            ViewData["UserID"] = userID;
            ViewData["accountType"] = accountType;
            return View();
        }

        public IActionResult Login()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
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
    }
}
