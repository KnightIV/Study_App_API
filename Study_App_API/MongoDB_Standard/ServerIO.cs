using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Data.SqlClient;
using MongoDB_Standard.models;
using MongoDB.Bson.Serialization;

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
        public static void CreateNote(Note note)
        {
            BsonDocument bnote = note.ToBsonDocument();
            IMongoCollection<BsonDocument> noteCollection = GetCollection(NOTE_COLLECTION);

            noteCollection.InsertOne(bnote);
        }
        public static void UploadFile(File file)
        {
            BsonDocument bfile = file.ToBsonDocument();
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);

            fileCollection.InsertOne(bfile);
        }

        public static void CreateUser(UserAccount user)
        {
            BsonDocument bUserAccount = user.ToBsonDocument();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);

            userCollection.InsertOne(bUserAccount);
        }

        public static void CreateGoal(Goal goal, string username)
    
        {
            BsonDocument bgoal = goal.ToBsonDocument();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
           
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("Username", username);


            BsonArray dataFields = new BsonArray { bgoal };
            UpdateDefinition<BsonDocument> update = new BsonDocument("$set", new BsonDocument { { "ListOfGoals", dataFields } });


            Console.WriteLine("Usernam Filter: " + getUserFilter);


            userCollection.UpdateOne(getUserFilter, update, new UpdateOptions { IsUpsert = true });

        }

        public static void MarkGoalAsComplete(string goalGuid, string Username)
        {
            throw new NotImplementedException();
        }

        public static bool AuthenticateUser(string Username, string Password)
        {
            throw new NotImplementedException();
        }

        public static void ShareFile(string guid, Dictionary<string, Permission> Sharers)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUser(string userName)
        {
            Console.WriteLine("Started method..." + " " + userName);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", userName);
            BsonDocument user = userCollection.Find(getUserFilter).First();

            var userGoalsList = user["ListOfGoals"].AsBsonArray;
            var userFilesList = user["ListOfFiles"].AsBsonValue;
            var userNotesList = user["ListOfNotes"].AsBsonValue;
            var username = user["UserName"].AsBsonValue;
            var phoneNumber = user["PhoneNumber"].AsBsonValue;
            var email = user["Email"].AsBsonValue;

            List<Goal> listOfGoals = new List<Goal>();


            foreach (var element in userGoalsList)
            {
                Goal g = null;
                var type = element["_t"].AsString;
                if (type == "NonRecurringGoal")
                {
                    g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                }
                else if (type == "RecurringGoal")
                {
                    g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                }
                listOfGoals.Add(g);
            }

            List<File> listOfFiles = BsonSerializer.Deserialize<List<File>>(userFilesList.ToJson());
            List<Note> listOfNotes = BsonSerializer.Deserialize<List<Note>>(userNotesList.ToJson());
            string usernameStr = BsonSerializer.Deserialize<string>(username.ToJson());
            string phoneNumberStr = BsonSerializer.Deserialize<string>(phoneNumber.ToJson());
            string emailStr = BsonSerializer.Deserialize<string>(email.ToJson());

            UserAccount userAccount = new UserAccount() { ListOfGoals = listOfGoals, ListOfFiles = listOfFiles, ListOfNotes = listOfNotes, UserName = usernameStr, PhoneNumber = phoneNumberStr, Email = emailStr};
            

            return userAccount;
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