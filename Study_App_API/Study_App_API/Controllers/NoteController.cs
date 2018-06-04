using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using MongoDB_Standard.models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class NoteController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpGet]
        public JsonResult GetNote(string username, string guid) {
            UserAccount user = serverInterface.GetUser(username);
            return Json(user.ListOfNotes.SingleOrDefault(n => n.GUID == guid), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetNotePreviews(string username) {
            UserAccount user = serverInterface.GetUser(username);
            List<NoteMini> preview = new List<NoteMini>();
            foreach (Note note in user.ListOfNotes) {
                preview.Add(note);
            }

            return Json(preview, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public void CreateNote([FromBody] Note note, string username) {
            serverInterface.CreateNote(note);
        }

        [System.Web.Mvc.HttpPost]
        public void DeleteNote(string guid) {
            serverInterface.DeleteNote(guid);
        }

        [System.Web.Mvc.HttpPost]
        public void UpdateNote(Note n) {
            // TODO: add way to update note
        }
    }
}