using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.DataAccess
{
    public class Departments
    {
        [BsonId]
        public ObjectId internalId { get; set; }
        [BsonElement]
        public string departmentName { get; set; }
        [BsonElement]
        public int numberofEmployees { get; set; }
        [BsonElement]
        public ObjectId managerId { get; set; }
        [BsonElement]
        public string managerName { get; set; }
    }
}
