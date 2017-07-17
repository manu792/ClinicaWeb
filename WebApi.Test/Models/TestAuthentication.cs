using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Models
{
    public class TestAuthentication : IAuthentication
    {
        public bool Authenticate(Credentials credentials)
        {
            return credentials.Username == "test" && credentials.Password == "test";
        }
    }
}
