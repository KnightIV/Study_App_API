using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;

namespace Study_App_API.MongoDB_Commands
{
    public class DatabaseCommand
    {

        string ConnectionString = null;
        MongoDatabase database = null;
        MongoServer server = null;
        MongoClient client = null;

        public DatabaseCommand(string DatabaseName)
        {
            Start_Mongo_Connection(DatabaseName);
        }

        public void Start_Mongo_Connection(string DatabaseName)
        {
            Console.WriteLine("Mongo DB Test Application");
            Console.WriteLine("====================================================");
            Console.WriteLine("Configuration Setting: 40.114.29.68:27017");
            Console.WriteLine("====================================================");
            Console.WriteLine("Initializaing connection");
            ConnectionString = "mongodb://40.114.29.68:27017";
            Console.WriteLine("Creating Client..........");

            CreateClient();

            Console.WriteLine("Initianting Mongo Db Server.......");
            CreateServer();

            Console.WriteLine("Initianting Mongo Databaser.........");
            CreateDataBase(DatabaseName);
        }

        private void CreateClient()
        {
            client = null;
            try
            {
                client = new MongoClient(ConnectionString);
                Console.WriteLine("Client Created Successfuly........");
                Console.WriteLine("Client: " + client.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Filed to Create Client.......");
                Console.WriteLine(ex.Message);
            }
        }
        private void CreateServer()
        {
            server = null;
            try
            {
                Console.WriteLine("Getting Servicer object......");
#pragma warning disable CS0618 // Type or member is obsolete
                server = client.GetServer();
#pragma warning restore CS0618 // Type or member is obsolete

                Console.WriteLine("Server object created Successfully....");
                Console.WriteLine("Server :" + server.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Filed to getting Server Details");
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateDataBase(string DatabaseName)
        {
            database = null;
            try
            {
                Console.WriteLine("Getting reference of database.......");
                database = server.GetDatabase(DatabaseName);
                Console.WriteLine("Database Name : " + database.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Get reference of Database");
                Console.WriteLine("Error :" + ex.Message);
            }

        }
        public void CreateCollection(string CollectionName)
        {
            try
            {
                Console.WriteLine("Creating Collection : " + CollectionName);
                database.CreateCollection(CollectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create collection from Database");
                Console.WriteLine("Error :" + ex.Message);
            }
        }

        public void DeleteCollection(string CollectionName)
        {
            try
            {
                Console.WriteLine("Deleting Collection : " + CollectionName);
                database.DropCollection(CollectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete collection from Database");
                Console.WriteLine("Error :" + ex.Message);
            }
        }
    }
}