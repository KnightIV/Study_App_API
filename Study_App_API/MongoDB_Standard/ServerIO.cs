using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Data.SqlClient;

namespace Study_App_API.MongoDB_Commands
{
    public class ServerIO
    {

        const string USER_COLLECTION = "UserAccount";
        const string NOTE_COLLECTION = "Note";
        const string FILE_COLLECTION = "File";

        const string MONGO_CONNECTION_STRING = "mongodb://40.114.29.68:27017";
        const string MONGO_DATABASE = "Mongo_Study_App";

        public static void DeleteFile(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);
            fileCollection.DeleteOne(deleteFileFilter);

        }
        public static void DeleteNote(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteNoteFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);
            fileCollection.DeleteOne(deleteNoteFilter);
        }
        public static void CreateNote(BsonDocument note)
        {
            IMongoCollection<BsonDocument> noteCollection = GetCollection(NOTE_COLLECTION);

            noteCollection.InsertOne(note);
        }
        public static void UploadFile(BsonDocument file)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);

            fileCollection.InsertOne(file);
        }

        public static void CreateUser(BsonDocument User)
        {
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);

            userCollection.InsertOne(User);
        }

        public static void CreateGoal(BsonDocument goal, string username)
        {
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
           
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("Username", username);


            BsonArray dataFields = new BsonArray { goal };
            UpdateDefinition<BsonDocument> update = new BsonDocument("$set", new BsonDocument { { "ListOfGoals", dataFields } });


            Console.WriteLine("Usernam Filter: " + getUserFilter);


            userCollection.UpdateOne(getUserFilter, update, new UpdateOptions { IsUpsert = true });

        }

        public void AddUserToFileDictionary(string fileGuid, string username, string permissionType)
        {
            //adds user to the Users dictionary on File Collection
            throw new NotImplementedException();
        }

        public void RemoveUserFromFileDictionary(string fileGuid, string removingUsername)
        {
            //edit dictionary to remove that user from the file--- file collections
            throw new NotImplementedException();
        }

        public void ChangePermissionForUser(string fileGuid, string username, string permissionType)
        {
            //edit the permission type for that user
            throw new NotImplementedException();
        }

        public static void MarkGoalAsComplete(string goalGuid, string Username)
        {
            throw new NotImplementedException();
        }

        public static bool AuthenticateUser(string Username, string Password)
        {
            throw new NotImplementedException();
        }

        public static void ShareFile(string guid, Dictionary<BsonDocument, BsonDocument> Sharers)
        {
            throw new NotImplementedException();
        }

        public static BsonDocument GetUser(string Username)
        {
            throw new NotImplementedException();
        }


        public static IMongoCollection<BsonDocument> GetUpcomingGoals(string Username)
        {
            throw new NotImplementedException();
        }

        public static IMongoCollection<BsonDocument> GetFilePreviews(string Username)
        {
            throw new NotImplementedException();
        }

        private static IMongoCollection<BsonDocument> GetCollection(string name)
        {
            MongoUrl url = new MongoUrl(MONGO_CONNECTION_STRING);
            MongoClient client = new MongoClient(url);
            IMongoDatabase db = client.GetDatabase(MONGO_DATABASE);
            return db.GetCollection<BsonDocument>(name);

        }
    }
}