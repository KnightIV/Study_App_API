using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Standard.models {

    public class FileMini {
        
        [BsonId]
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
    }
}
