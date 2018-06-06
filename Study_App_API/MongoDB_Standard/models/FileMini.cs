using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace StudyApp.Assets.Models {

    public class FileMini {
        
        [BsonId]
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
