using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using StudyApp.Assets.Models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class UserController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpGet]
        public JsonResult AuthenticateUser(string username, string password) {
            LoginUser user = serverInterface.GetLoginUser(username);
            if (user == null)
                return Json(false, JsonRequestBehavior.AllowGet);
            string hashedPassword = EncryptPassword(password, user.Salt).hashedPassword;
            return Json(serverInterface.AuthenticateUser(username, hashedPassword) , JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ContentResult CreateAccount([FromBody] UserAccount user, string password) {
            var (hashedPassword, salt) = EncryptPassword(password); 
            serverInterface.CreateUser(user, hashedPassword, salt);
            return SerializeUser(user);
        }

        [System.Web.Mvc.Route("doesuserexist/{username}")]
        [System.Web.Mvc.HttpGet]
        public JsonResult DoesUserExist(string username) {
            return Json(serverInterface.GetUser(username) != null, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public ContentResult GetUser(string username) {
            return SerializeUser(serverInterface.GetUser(username));
        }

        [System.Web.Mvc.HttpPost]
        public void AddPoints(string username, int pointsToAdd) {
            throw new NotImplementedException();
        }

        private ContentResult SerializeUser(UserAccount user) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented,
                SerializationBinder = new SerializationBinderHelper()
            };
            return Content(JsonConvert.SerializeObject(user, settings), "application/json");
        }

        private byte[] CreateSalt(int size = 10) {
            byte[] buffer = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(buffer);
                return buffer;
            }
        }

        private (string hashedPassword, byte[] salt) EncryptPassword(string password, byte[] salt = null) {
            HashAlgorithm hashAlgorithm = new SHA256Managed();
            byte[] saltBytes = salt ?? CreateSalt();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] passwordAndSaltBytes = new byte[saltBytes.Length + passwordBytes.Length];
            passwordBytes.CopyTo(passwordAndSaltBytes, 0);
            saltBytes.CopyTo(passwordAndSaltBytes, passwordBytes.Length);

            byte[] hashedPassword = hashAlgorithm.ComputeHash(passwordAndSaltBytes);
            return (Convert.ToBase64String(hashedPassword), saltBytes);
        }
    }
}