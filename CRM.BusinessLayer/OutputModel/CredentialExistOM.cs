using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.BusinessLayer.OutputModel
{
    public class CredentialExistOM
    {
        public bool userNameExist { get; set; }

        public bool emailExist { get; set; }

        public string message { get; set; }
    }
}
