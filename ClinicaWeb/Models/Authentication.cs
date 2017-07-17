using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class Authentication : IAuthentication
    {
        public bool Authenticate(Credentials credentials)
        {
            return credentials.Username == ConfigurationManager.AppSettings["username"] && credentials.Password == ConfigurationManager.AppSettings["password"];
        }
    }
}