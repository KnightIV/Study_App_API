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

        [System.Web.Mvc.HttpPost]
        public void CreateGoal(string username, [FromBody] Goal g) {
            serverInterface.CreateGoal(g, username);
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
        public JsonResult GetUpcomingRecurringGoals(string username, string dateString) {
            DateTime curDate = Convert.ToDateTime(dateString);
            UserAccount user = serverInterface.GetUser(username);
            int curMonth = curDate.Month;
            Dictionary<Goal, DateTime> recurringGoals = new Dictionary<Goal, DateTime>();
            foreach (Goal goal in user.ListOfGoals) {
                if (goal is RecurringGoal recGoal) {
                    DateTime date = recGoal.Deadline;
                    for (; date.Month < curMonth; date += recGoal.Frequency) { }

                    if (date.Month == curMonth) {
                        recurringGoals.Add(goal, date);
                    }
                }
            }

            return Json(recurringGoals, JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Mvc.HttpGet]
        public JsonResult GetUpcomingNonRecurringGoals(string username, string dateString) {
            DateTime curDate = Convert.ToDateTime(dateString);
            List<Goal> upcomingGoals = serverInterface.GetUpcomingGoals(username, curDate);
            return Json(upcomingGoals.Where(g => g is NonRecurringGoal).ToList(), JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Mvc.HttpGet]
        public JsonResult GetOverdueGoals(string username, string dateString) {
            DateTime curDate = Convert.ToDateTime(dateString);
            UserAccount user = serverInterface.GetUser(username);
            List<Goal> overdueGoals = user.ListOfGoals.Where(g => g is NonRecurringGoal && g.Deadline < curDate).ToList();
            return Json(overdueGoals, JsonRequestBehavior.AllowGet);
        }
    }
}