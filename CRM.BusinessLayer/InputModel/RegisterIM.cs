using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.BusinessLayer.InputModel
{
    public class RegisterIM
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string department { get; set; }
        [Required]
        public string hierarchy { get; set; }
    }
}
