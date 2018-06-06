using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Study_App_API.Controllers {

    internal class ByteArrayConverter : JsonConverter {

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            string base64String = Convert.ToBase64String((byte[]) value);

            serializer.Serialize(writer, base64String);
        }

        public override bool CanRead {
            get { return false; }
        }

        public override bool CanConvert(Type t) {
            return typeof(byte[]).IsAssignableFrom(t);
        }
    }

    public class SerializationBinderHelper : ISerializationBinder {

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented,
            SerializationBinder = new SerializationBinderHelper(),
            Converters = { new ByteArrayConverter() }
        };

        public Type BindToType(string assemblyName, string typeName) {
            return Type.GetType(typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName) {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}