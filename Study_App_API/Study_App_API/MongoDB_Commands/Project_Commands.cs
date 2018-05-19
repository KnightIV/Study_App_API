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


    public class Project_Commands
    {

        string USER_COLLECTION;
        string NOTE_COLLECTION;
        string FILE_COLLECTION;

        public Project_Commands()
        {
         
        }

        public static void DeleteFile(string guid)
        {
            throw new NotImplementedException();
        }

        public static BsonDocument DeleteNote(string guid)
        {
            throw new NotImplementedException();
        }

        public static void CreateNote(BsonDocument Note)
        {
            throw new NotImplementedException();
        }
        
        public static void UploadFile(BsonDocument File)
        {
            throw new NotImplementedException();
        }

        public static void CreateUser(BsonDocument User)
        {
            throw new NotImplementedException();
        }

        public static void CreateGoal(BsonDocument Goal, string Username)
        {
            throw new NotImplementedException();
        }

        public static void MarkGoalAsComplete(string guid, string Username)
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

        public static IMongoCollection<BsonDocument> GetCollection(string collection)
        {
            throw new NotImplementedException();
        }
    }
}