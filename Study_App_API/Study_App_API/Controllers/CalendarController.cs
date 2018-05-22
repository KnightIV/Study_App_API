using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Study_App_API.MongoDB_Commands;
using MongoDB_Standard.models;

namespace Study_App_API.Controllers {

    public class CalendarController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [HttpGet]
        public JsonResult GetMonth(string username, int monthOfYear) {
            Month month = new Month();
            throw new NotImplementedException();
        }

        private List<Goal> GetMonthRecurringGoals() {
            throw new NotImplementedException();
        }
    }
}