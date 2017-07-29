using ClinicaWeb.Models;
using System.Web.Http;

namespace ClinicaWeb.Controllers
{
    [AllowAnonymous]
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
                return Ok(new AuthenticationResponse() { Success = false, Message = "Username or password is incorrect" });
            }

            return Ok(new AuthenticationResponse() { Success = true });
        }
    }
}