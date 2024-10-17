using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
