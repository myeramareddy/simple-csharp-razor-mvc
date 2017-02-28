using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloEthos.Models;

namespace HelloEthos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "A Java-n building a ASP.NET MVC application.";

            return View();
        }

        public ActionResult FunStrings()
        {
            //ViewBag.Message = "fun with strings viewbag message.";

            return View();
        }

        public ActionResult PlayStrings(string stringToChange)
        {
            //ViewBag.Message = "playStrings viewbag mess.";
            return View(new FunStringsModel(stringToChange, false));
        }

    }
}
