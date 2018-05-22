using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Study_App_API.MongoDB_Commands;
using MongoDB_Standard.models;

namespace Study_App_API.Controllers {

    public class GoalController : Controller {

        private ServerIO serverInterface = new ServerIO();

        // TODO: some testing on passing in a goal through JSON in an HTTP request
        [System.Web.Mvc.HttpPost]
        public void CreateGoal(string username, [FromBody] Goal g) {
            serverInterface.CreateGoal(g, username);
            // TODO: decide if this method will change the user or if the serverInterface should be in charge of that
        }

        [System.Web.Mvc.HttpPost]
        public void CompleteGoal(string username, string goalGuid) {
            serverInterface.MarkGoalAsComplete(goalGuid, username);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetGoal(string guid, string username) {
            UserAccount user = serverInterface.GetUser(username);
            return Json(user.ListOfGoals.SingleOrDefault(g => g.GUID == guid), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetUpcomingRecurringGoals(string username, DateTime curDate) {
            // TODO: fix return type of serverInterface.GetUpcomingGoals()
            List<Goal> upcomingGoals = (List<Goal>) serverInterface.GetUpcomingGoals(username, curDate);
            return Json(upcomingGoals.Where(g => g is RecurringGoal).ToList(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetUpcomingNonRecurringGoals(string username, DateTime curDate) {
            // TODO: fix return type of serverInterface.GetUpcomingGoals()
            List<Goal> upcomingGoals = (List<Goal>) serverInterface.GetUpcomingGoals(username, curDate);
            return Json(upcomingGoals.Where(g => g is NonRecurringGoal).ToList(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetOverdueGoals(string username, DateTime curDate) {
            UserAccount user = serverInterface.GetUser(username);
            List<Goal> overdueGoals = user.ListOfGoals.Where(g => g is NonRecurringGoal && g.Deadline < curDate).ToList();
            return Json(overdueGoals, JsonRequestBehavior.AllowGet);
        }
    }
}