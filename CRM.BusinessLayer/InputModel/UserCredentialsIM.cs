using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.BusinessLayer.InputModel
{
    public class UserCredentialsIM
    {
        public string firstName { get; set; }
        
        public string lastName { get; set; }
        
        public string userName { get; set; }
        
        public string email { get; set; }

        public string password { get; set; }

        public string hierarchy { get; set; }
        
        public string department { get; set; }
    }
}
