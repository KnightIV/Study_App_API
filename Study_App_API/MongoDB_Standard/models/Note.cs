using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyApp.Assets.Models {

    public class Note {

        [BsonId]
        public string GUID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public  string Owner { get; set; }

        public Note(string owner, string title = null, string content = null, string guid = null) {
            Owner = owner;
            Title = title;
            Content = content;
            GUID = guid;
        }

        public Note() { }

        public static implicit operator NoteMini(Note n) => new NoteMini(n.Title, n.GUID);
    }
}
