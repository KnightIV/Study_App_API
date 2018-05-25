using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using MongoDB_Standard.models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class UserController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [System.Web.Mvc.HttpGet]
        public JsonResult AuthenticateUser(string username, string password) {
            UserAccount user = serverInterface.GetUser(username);
            if (user == null)
                return Json(false, JsonRequestBehavior.AllowGet);
            string hashedPassword = EncryptPassword(password, null).hashedPassword; // replace salt null with loginuser stuff
            return Json(serverInterface.AuthenticateUser(username, hashedPassword) , JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult CreateAccount([FromBody] UserAccount user, string password) {
            var (hashedPassword, salt) = EncryptPassword(password); 
            serverInterface.CreateUser(user, hashedPassword, salt);
            return Json(user);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult DoesUserExist(string username) {
            return Json(new { result = serverInterface.GetUser(username) == null }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetUser(string username) {
            return Json(serverInterface.GetUser(username), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public void AddPoints(string username, int pointsToAdd) {
            UserAccount user = serverInterface.GetUser(username);
            user.Points += pointsToAdd;
            // TODO: add a way to update users
        }

        private byte[] CreateSalt(int size = 10) {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[size];
            rng.GetBytes(buffer);
            return buffer;
        }

        private (string hashedPassword, string salt) EncryptPassword(string password, string salt = null) {
            HashAlgorithm hashAlgorithm = new SHA256Managed();
            byte[] saltBytes;
            if (salt == null) {
                saltBytes = CreateSalt();
            } else {
                saltBytes = Encoding.UTF8.GetBytes(salt);
            }
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] passwordAndSaltBytes = new byte[saltBytes.Length + passwordBytes.Length];
            passwordBytes.CopyTo(passwordAndSaltBytes, 0);
            saltBytes.CopyTo(passwordAndSaltBytes, passwordBytes.Length);

            byte[] hashedPassword = hashAlgorithm.ComputeHash(passwordAndSaltBytes);
            return (Convert.ToBase64String(hashedPassword), Convert.ToBase64String(saltBytes));
        }
    }
}