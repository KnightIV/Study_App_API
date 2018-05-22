using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MongoDB_Standard.models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class NoteController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [HttpGet]
        public JsonResult GetNote(string username, string guid) {
            UserAccount user = serverInterface.GetUser(username);
            return Json(user.ListOfNotes.SingleOrDefault(n => n.GUID == guid), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNotePreviews(string username) {
            UserAccount user = serverInterface.GetUser(username);
            List<NoteMini> preview = new List<NoteMini>();
            foreach (Note note in user.ListOfNotes) {
                preview.Add(note);
            }

            return Json(preview, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CreateNote(Note note, string username) {
            // TODO: add way to update user
        }

        [HttpPost]
        public void DeleteNote(string guid) {
            // TODO: add way to update user
        }

        [HttpPost]
        public void UpdateNote(Note n) {
            // TODO: add way to update note
        }
    }
}