using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Newtonsoft.Json;

using Study_App_API.MongoDB_Commands;
using StudyApp.Assets.Models;

namespace Study_App_API.Controllers {

    public class GoalController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpPost]
        public void CreateNonRecurringGoal(string username, [FromBody] NonRecurringGoal g) {
            serverInterface.CreateGoal(g, username);
        }

        [System.Web.Mvc.HttpPost]
        public void CreateRecurringGoal(string username, [FromBody] RecurringGoal g) {
            serverInterface.CreateGoal(g, username);
        }

        [System.Web.Mvc.HttpPost]
        public void CompleteGoal(string username, string goalGuid) {
            serverInterface.MarkGoalAsComplete(goalGuid, username);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult GetGoal(string guid, string username) {
            UserAccount user = serverInterface.GetUser(username);
            return Content(JsonConvert.SerializeObject(user.ListOfGoals.SingleOrDefault(g => g.GUID == guid), SerializationBinderHelper.Settings), "application/json");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult GetUpcomingRecurringGoals(string username, string dateString)
        {
            DateTime curDate = Convert.ToDateTime(dateString);
            List<Goal> upcomingGoals = serverInterface.GetUpcomingGoals(username, curDate);
            //UserAccount user = serverInterface.GetUser(username);
            //int curMonth = curDate.Month;
            //Dictionary<Goal, DateTime> recurringGoals = new Dictionary<Goal, DateTime>();
            //foreach (Goal goal in user.ListOfGoals) {
            //    if (goal is RecurringGoal recGoal) {
            //        DateTime date = recGoal.Deadline;
            //        for (; date.Month < curMonth; date += recGoal.Frequency) { }

            //        if (date.Month == curMonth) {
            //            recurringGoals.Add(goal, date);
            //        }
            //    }
            //}

            return Content(JsonConvert.SerializeObject(upcomingGoals.Where(g => g is RecurringGoal).ToList(), SerializationBinderHelper.Settings));
        }
        
        [System.Web.Mvc.HttpGet]
        public ActionResult GetUpcomingNonRecurringGoals(string username, string dateString)
        {
            DateTime curDate = Convert.ToDateTime(dateString);
            List<Goal> upcomingGoals = serverInterface.GetUpcomingGoals(username, curDate);
            return Content(JsonConvert.SerializeObject(upcomingGoals.Where(g => g is NonRecurringGoal).ToList(), SerializationBinderHelper.Settings));
        }
        
        [System.Web.Mvc.HttpGet]
        public ActionResult GetOverdueGoals(string username, string dateString)
        {
            DateTime curDate = Convert.ToDateTime(dateString);
            UserAccount user = serverInterface.GetUser(username);
            List<Goal> overdueGoals = user.ListOfGoals.Where(g => g is NonRecurringGoal && g.Deadline > curDate).ToList();
            return Content(JsonConvert.SerializeObject(overdueGoals, SerializationBinderHelper.Settings));
        }
    }
}