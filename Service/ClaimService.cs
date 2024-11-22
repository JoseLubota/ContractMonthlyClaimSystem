using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace ContractMonthlyClaimSystem.Service;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Azure;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;

public interface IClaimService
{
    List<ClaimModel> GetClaimsByLecturer(int lecturerID);
    byte[] GenerateClaimReport(List<ClaimModel> claims);
}
public class ClaimService : IClaimService
{
    private readonly AppDbContext _context;
    public ClaimService(AppDbContext context)
    {
        _context = context;
    }

    public List<ClaimModel> GetClaimsByLecturer(int lecturerID)
    {
        return _context.Claims.Where(c => c.LECTURER_ID == lecturerID).ToList();
    }

    public byte[] GenerateClaimReport(List<ClaimModel> claims)
    {
        using (var stream = new MemoryStream())
        {
            var document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();
            document.Add(new Paragraph("                                        Lecturer Claim Report" + "\n\n\n"));
            foreach (var claim in claims)
            {
                document.Add(new Paragraph(
                    $"Claim ID: {claim.CLAIM_ID}" + "\n" +
                    $"Lecturer ID: {claim.LECTURER_ID}" + "\n" +
                    $"Hours Worked: {claim.HOURS_WORKED}" + "\n" +
                    $"Hours Worked: {claim.HOURS_WORKED}" + "\n" +
                    $"Hourly Rate: {claim.HOURLY_RATE}" + "\n" +
                    $"Final Payment: {Convert.ToDouble(claim.HOURS_WORKED) * Convert.ToDouble(claim.HOURLY_RATE)}" + "\n\n\n\n\n" +
                    $"Additiona Notes: {claim.NOTES}"));
            }
            document.Close();
            return stream.ToArray();
        }
    }

}


