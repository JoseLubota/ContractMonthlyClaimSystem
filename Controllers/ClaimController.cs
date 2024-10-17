using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
namespace ContractMonthlyClaimSystem.Controllers
{

    public class ClaimController : Controller
    {
        public claimTBL claimtbl = new claimTBL();
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public ActionResult insertClaim(claimTBL Claim)
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            var result = claimtbl.insert_Claim(Claim, userID);
            return RedirectToAction("Claim", "Home");
        }

        [HttpGet]
        public ActionResult insertClaim()
        {
            return View(claimtbl);
        }

        [HttpGet]
        public ActionResult Claim()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            List<ClaimModel> claims;
            claims = ClaimModel.SelectClaims();
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            return View();
        }

    }
}
