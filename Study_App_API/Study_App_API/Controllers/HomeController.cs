using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study_App_API.Controllers {

    public class HomeController : Controller {

        // ip/home/index
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
