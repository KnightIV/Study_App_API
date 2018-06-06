using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyApp.Assets.Models {

    public class LoginUser {

        [BsonId]
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
