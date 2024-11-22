using ContractMonthlyClaimSystem.Controllers;
using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ContractMonthlyClaimSystem.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Models
{
    public class claimTBL
    {
        private readonly AppDbContext _context;
        private readonly FileService _fileService;

        public claimTBL(AppDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public claimTBL(AppDbContext context)
        {
            _context = context;
        }
        public claimTBL()
        {
        }

        public int USERID { get; set; }
        public string STATUS { get; set; }
        public string APPROVER_ID { get; set; }
        public string LECTURER_ID { get; set; }
        public string NOTES { get; set; }
        public string HOURS_WORKED { get; set; }
        public string HOURLY_RATE { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; } = DateTime.Now;

        public async Task<int?> GetLastClaimIDAsync()
        {
            var lastClaim = await _context.Claims
                .OrderByDescending(c => c.CLAIM_ID)
                .FirstOrDefaultAsync();

            return lastClaim == null ? 1 : lastClaim.CLAIM_ID + 1;

        }
        public async Task<int> insert_Claim(claimTBL c, int? userID, IFormFile document, FileService fileService)
        {
            try
            {
                int lecturerID = Convert.ToInt32(userID);
                string fileName = null;
                TIMESTAMP = DateTime.Now;
                int? claimID = await GetLastClaimIDAsync();
                if (document != null && document.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                    fileName = "Claim_" + lecturerID + "_" + DateTime.Now.Ticks + "_" + document.FileName;
                    await fileService.UploadFileAsync(filePath, fileName);
                }
                string documentName = fileName ?? "none";

                var claim = new ClaimModel
                {
                    CLAIM_ID = Convert.ToInt32(claimID),
                    LECTURER_ID = lecturerID,
                    NOTES = c.NOTES,
                    HOURLY_RATE = c.HOURLY_RATE,
                    HOURS_WORKED = c.HOURS_WORKED,
                    STATUS = "PENDING",
                    DOCUMENT_NAME = documentName,
                    TIMESTAMP = DateTime.Now
                };

                _context.Claims.Add(claim);
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ClaimModel>> GetClaimsAsync()
        {
            if (_context.Claims == null)
            {
                return new List<ClaimModel>();
            }
            else
            {
                var claims = await _context.Claims
                    .Select(c => new ClaimModel
                    {
                        CLAIM_ID = c.CLAIM_ID,
                        STATUS = c.STATUS,
                        LECTURER_ID = c.LECTURER_ID,
                        APPROVER_ID = c.APPROVER_ID,
                        NOTES = c.NOTES,
                        HOURS_WORKED = c.HOURS_WORKED,
                        HOURLY_RATE = c.HOURLY_RATE,
                        DOCUMENT_NAME = c.DOCUMENT_NAME,
                        TIMESTAMP = c.TIMESTAMP
                    })
                    .ToListAsync();
                return claims;
            }

        }
        //
        public async Task<List<ClaimModel>> SelectClaimsAsync()
        {

            return await _context.Claims
                .Include(c => c.APPROVER_ID)
                .Include(c => c.LECTURER_ID)
                .ToListAsync();

        }

        public async Task<bool> updatedClaimsAsync(int CLAIM_ID, string STATUS, int APPROVER_ID)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.CLAIM_ID == CLAIM_ID);
            if (claim != null)
            {
                claim.STATUS = STATUS;
                claim.APPROVER_ID = APPROVER_ID;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<int> InsertClaimAsync(ClaimModel claim)
        {
            _context.Claims.Add(claim);
            return await _context.SaveChangesAsync();
        }

    }


}
