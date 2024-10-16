using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class UserController : Controller
    {
        public cmcs_userTBL usertbl = new cmcs_userTBL();

        [HttpPost]
        public ActionResult SignUp(cmcs_userTBL Users)
        {
            var result = usertbl.insert_User(Users);
            return RedirectToAction("SignUp", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usertbl);
        }

       

    }
}
