using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using StudyApp.Assets.Models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class FileController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpPost]
        public void UploadFile([FromBody] File f) {
            serverInterface.UploadFile(f);
        }
        
        [System.Web.Mvc.HttpGet]
        public ActionResult DownloadFile(string guid) {
            return Content(JsonConvert.SerializeObject(serverInterface.GetFileFromCollection(guid), SerializationBinderHelper.Settings), "application/json");
        }
        
        [System.Web.Mvc.HttpPost]
        public void DeleteFile(string guid) {
            serverInterface.DeleteFile(guid);
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetFilePreviews(string username) {
            return Content(JsonConvert.SerializeObject(serverInterface.GetFilePreviews(username), SerializationBinderHelper.Settings), "application/json");
        }

        [System.Web.Mvc.HttpPost]
        public void ShareFile(string guid, [FromBody] Dictionary<string, Permission> shareWith) {
            serverInterface.ShareFile(guid, shareWith);
        }
    }
}