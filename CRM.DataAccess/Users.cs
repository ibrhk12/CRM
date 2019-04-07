using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRM.DataAccess
{
    public class Users
    {
        [BsonId]
        public ObjectId internalId { get; set; }
        [BsonElement]
        public string firstName { get; set; }
        [BsonElement]
        public string lastName { get; set; }
        [BsonElement]
        public string userName { get; set; }
        [BsonElement]
        public string email { get; set; }
        [BsonElement]
        public string password { get; set; }
        [BsonElement]
        public string hierarchy { get; set; }
        [BsonElement]
        public string department { get; set; }
    }
}
