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
    }
}