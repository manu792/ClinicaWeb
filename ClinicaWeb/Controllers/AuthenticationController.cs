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
        //We are currently authenticating against Web.config, but we could change data source.
        private IAuthentication Authentication { get; set; }

        public AuthenticationController(IAuthentication authentication)
        {
            Authentication = authentication;
        }

        public IHttpActionResult PostAuthentication(Credentials credentials)
        {
            // Looks for username and password in the Web.config file
            if (!Authentication.Authenticate(credentials))
            {
                return Ok(new AuthenticationFailed() { Success = false, Message = "Username or password is incorrect" });
            }

            return Ok(new AuthenticationSuccessful() { Success = true });
        }
    }
}