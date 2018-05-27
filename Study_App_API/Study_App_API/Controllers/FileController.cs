using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using MongoDB_Standard.models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class FileController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpPost]
        public void UploadFile([FromBody] File f) {
            serverInterface.UploadFile(f);
        }
        
        [System.Web.Mvc.HttpGet]
        public JsonResult DownloadFile(string guid) {
            return Json(serverInterface.GetFileFromCollection(guid), JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Mvc.HttpPost]
        public void DeleteFile(string guid) {
            serverInterface.DeleteFile(guid);
        }

        [System.Web.Http.HttpGet]
        public JsonResult GetFilePreviews(string username) {
            return Json(serverInterface.GetFilePreviews(username), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public void ShareFile(string guid, [FromBody] Dictionary<string, Permission> shareWith) {
            serverInterface.ShareFile(guid, shareWith);
        }
    }
}