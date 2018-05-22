using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using MongoDB_Standard.models;
using Study_App_API.MongoDB_Commands;

namespace Study_App_API.Controllers {

    public class UserController : Controller {

        private ServerIO serverInterface = new ServerIO();

        [HttpGet]
        public JsonResult Login(string username, string password) {
            UserAccount user = serverInterface.GetUser(username);
            if (user == null)
                return Json(false, JsonRequestBehavior.AllowGet);

            string hashedPassword = EncryptPassword(password, null); // replace with loginuser stuff
            return Json(serverInterface.AuthenticateUser(username, hashedPassword) , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateAccount(string username, string password, string email, string phoneNumber = null) {
            UserAccount newUser = new UserAccount {
                UserName = username,
                Email = email,
                PhoneNumber = phoneNumber ?? String.Empty
            };
            serverInterface.CreateUser(newUser);
            return Json(newUser);
        }

        [HttpGet]
        public JsonResult DoesUserExist(string username) {
            return Json(new { result = serverInterface.GetUser(username) == null }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUser(string username) {
            return Json(serverInterface.GetUser(username), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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

        private string EncryptPassword(string password, string salt = null) {
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
            return Convert.ToBase64String(hashedPassword);
        }
    }
}