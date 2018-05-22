using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class UserController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [HttpGet]
        public JsonResult Login(string username, string password) {
            //return Json(serverInterface.AuthenticateUser(username, password));
            throw new NotImplementedException();
        }
    }
}