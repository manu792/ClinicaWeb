using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClinicaWeb.Controllers
{
    public class AuthenticationController : ApiController
    {
        public IHttpActionResult PostAuthentication(Authentication authentication)
        {
            // Looks for username and password in the Web.config file
            if (!(authentication.Username == ConfigurationManager.AppSettings["username"] && authentication.Password == ConfigurationManager.AppSettings["password"]))
            {
                return Ok(new { success = false, message = "Username or password is incorrect" });
            }

            return Ok(new { success = true });
        }
    }
}