using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.BusinessLayer.InputModel
{
    public class LoginIM
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
