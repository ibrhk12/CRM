﻿using CRM.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRM.BusinessLayer
{
    public class CRMContext
    {
        private readonly IMongoDatabase _database = null;
        public CRMContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }
        //Users 
        public IMongoCollection<Users> Users
        {
            get
            {
                return _database.GetCollection<Users>("Users");
            }
        }
        //Department
        public IMongoCollection<Departments> Department
        {
            get
            {
                return _database.GetCollection<Departments>("Department");
            }
        }
    }
}
