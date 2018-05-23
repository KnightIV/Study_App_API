using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Standard.models {
    [BsonIgnoreExtraElements]
    public class File {

        [BsonId]
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public Dictionary<string, Permission> Users { get; set; }
        public byte[] Content { get; set; }

        public static implicit operator FileMini(File f) => new FileMini() { GUID = f.GUID, Extension = f.Extension, Name = f.Name };
    }
}
