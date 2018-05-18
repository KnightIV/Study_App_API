using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;

namespace Study_App_API.MongoDB_Commands
{


    public class Project_Commands
    {
        IMongoDatabase mongo;
        public Project_Commands(string ConnectionString)
        {

        }

        public void  DeleteFile(string Id)
        {
            throw new NotImplementedException();
        }
        public void DeleteNote(string Id)
        {
            throw new NotImplementedException();
        }

        public void CreateFile(string Id)
        {
            throw new NotImplementedException();
        }

        public void UploadFile(string File)
        {
            throw new NotImplementedException();
        }
        public void CreateUser(string User)
        {
            throw new NotImplementedException();
        }

        public void CreateGoals(string Goal)
        {
            throw new NotImplementedException();
        }

        public void MarkGoalAsComplete(string Goal)
        {
            throw new NotImplementedException();
        }

        public void AuthenticateUser(string AuthUser) {
            throw new NotImplementedException();
        } 

        public void ShareFile(String Id, String Dictionary)
        {
            throw new NotImplementedException();
        }

        public void GetUser(String Username)
        {
            throw new NotImplementedException();
        }

        public void GetUpComingGoals(string Username)
        {
            throw new NotImplementedException();
        }

        public void GetFilePreviews(string Username)
        {
            throw new NotImplementedException();
        }

        public void GetCollections(string Collection)
        {
            throw new NotImplementedException();
        }

    }
}