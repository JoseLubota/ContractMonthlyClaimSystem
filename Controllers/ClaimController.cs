using Azure;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContractMonthlyClaimSystem.Data;
using System.ComponentModel.DataAnnotations;
using System;
namespace ContractMonthlyClaimSystem.Controllers

{
    //[ApiController]
    //[ Route("api/controller")]
    public class ClaimController : Controller
    {
        private readonly claimTBL _claimTBL;
        private readonly IClaimService _claimService;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileService _fileService;
        private readonly IValidator<ClaimModel> _validator;
        private readonly AppDbContext _context;

        public ClaimController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, FileService azureFileService, claimTBL claimTBL, IValidator<ClaimModel> validator, AppDbContext context, IClaimService claimService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _fileService = azureFileService;
            _claimTBL = claimTBL ?? throw new ArgumentException(nameof(claimTBL));
            _validator = validator;
            _context = context;
            _claimService = claimService;
        }

        [HttpPost]
        public async Task<IActionResult> insertClaim(claimTBL Claim, IFormFile document, ClaimModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                _logger.LogWarning("Claim model validation failed: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                return BadRequest(new
                {
                    status = 400,
                    title = "Validation Error",
                    errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            if (userID == null)
            {

                return RedirectToAction("Claim", "Home");
            }
            try
            {
                var result = await _claimTBL.insert_Claim(Claim, userID, document, _fileService);
                if (result > 0)
                {
                    return RedirectToAction("Claim", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to submit claim.Plase try again.");
                    return View();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "bla bla");
                // ModelState.AddModelError("", "Model error");
                return View();
            }
            return RedirectToAction("Claim", "Home");
        }
        [HttpPost]
        public IActionResult GenerateReport(int LecturerID)
        {
            try
            {
                var claims = _claimService.GetClaimsByLecturer(LecturerID);
                var report = _claimService.GenerateClaimReport(claims);
                var fileName = $"Lecturer_{LecturerID}_Report.pdf";
                return File(report, "application/pdf", fileName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Claim", "Home");
            }
        }

        [HttpGet]
        public ActionResult insertClaim()
        {
            return View(_claimTBL);
        }


        [HttpGet]
        public async Task<ActionResult> Claim()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
            var claims = await _claimTBL.GetClaimsAsync(); ;
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            ViewData["accountType"] = accountType;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int CLAIM_ID, string STATUS, int APPROVER_ID)
        {
            var claim = _context.Claims.Find(CLAIM_ID);
            if (claim != null)
            {
                var workflow = new ClaimApprovalWorkflow();
                workflow.ApproveClaim(claim);
                claim.APPROVER_ID = APPROVER_ID;
                _context.SaveChanges();
            }
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            //var result = await _claimTBL.updatedClaimsAsync(CLAIM_ID, STATUS, APPROVER_ID);
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
            if (claim != null)
            {

                ViewData["UserID"] = userID;
                ViewData["Claims"] = _claimTBL.GetClaimsAsync();
                ViewData["accountType"] = accountType;
                return RedirectToAction("Claim", "Home");

            }
            ViewData["UserID"] = userID;
            ViewData["Claims"] = _claimTBL.GetClaimsAsync();
            ViewData["accountType"] = accountType;
            return RedirectToAction("Claim", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> updateStatus()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("userID");
            var claims = await _claimTBL.SelectClaimsAsync();
            LoginModel userTBL = new LoginModel();
            string accountType = userTBL.GetAccountType(userID);
            ViewData["UserID"] = userID;
            ViewData["Claims"] = claims;
            ViewData["accountType"] = accountType;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> DownloadDocument(string fileName)
        {
            try
            {
                var stream = await _fileService.DownloadFileAsync(fileName);
                return File(stream, "application/octet-stream", fileName);

            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                throw new Exception($"This file '{fileName}' does not exist.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while downloading the file.", ex);
            }

        }
    }

}
