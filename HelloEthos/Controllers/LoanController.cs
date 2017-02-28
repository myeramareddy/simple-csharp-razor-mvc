using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloEthos.Models;

namespace HelloEthos.Controllers
{
    public class LoanController : Controller
    {

        [HttpPost]
        public ActionResult YourSched(LoanTermModel loanInput)
        {
            List<LoanSchedModel> s = loanInput.getLoanScheduleForInputs();
            ViewBag.message = loanInput.toString();

            return View(s);
        }

        public ActionResult GetLoanInputs()
        {
            return View();
        }

    }
}
