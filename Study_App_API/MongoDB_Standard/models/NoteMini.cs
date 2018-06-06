using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyApp.Assets.Models {

    public class NoteMini {

        [BsonId]
        public string GUID { get; set; }
        public string Title { get; set; }

        public NoteMini(string title = null, string guid = null) {
            Title = title;
            GUID = guid;
        }
    }
}
