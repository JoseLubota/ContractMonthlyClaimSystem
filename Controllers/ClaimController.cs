using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Service;
using Microsoft.AspNetCore.Mvc;
namespace ContractMonthlyClaimSystem.Controllers
{

    public class ClaimController : Controller
    {
        public claimTBL claimtbl = new claimTBL();
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileService _fileService;

        public ClaimController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, FileService azureFileService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _fileService = azureFileService;
        }
        [HttpPost]
        public async Task<ActionResult> insertClaim(claimTBL Claim, IFormFile document)
        {

            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            var result = await claimtbl.insert_Claim(Claim, userID,document, _fileService);
            if(result > 0)
            {
                return RedirectToAction("Claim", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Unable to submit claim.Plase try again.");
                return View();
            }
           
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

        [HttpPost]
        public IActionResult updateStatus(int CLAIM_ID, string STATUS, int APPROVER_ID)
        {
            
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            List<ClaimModel> claims;
            claims = ClaimModel.updatedClaims(CLAIM_ID, STATUS, APPROVER_ID);
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            return RedirectToAction("Claim", "Home");
        }
        [HttpGet]
        public IActionResult updateStatus()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            List<ClaimModel> claims;
            claims = ClaimModel.SelectClaims();
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> DownloadDocument(string fileName)
        {
            var stream = await _fileService.DownloadFileAsync(fileName);
            return File(stream, "application/octet-stream", fileName);
        }
    }
}
