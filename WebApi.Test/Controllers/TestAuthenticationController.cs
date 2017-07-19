using ClinicaWeb.Controllers;
using ClinicaWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebApi.Test.Models;

namespace WebApi.Test.Controllers
{
    [TestClass]
    public class TestAuthenticationController
    {
        [TestMethod]

        public void TestPostAuthentication_ShouldReturnSuccessTrue()
        {

            var testAuthentication = new TestAuthentication();
            var credentials = new Credentials()
            {
                Username = "test",
                Password = "test"
            };

            var authenticationController = new AuthenticationController(testAuthentication);

            var result = authenticationController.PostAuthentication(credentials) as OkNegotiatedContentResult<AuthenticationResponse>;
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Content.Success);
            Assert.IsNull(result.Content.Message);
        }
        [TestMethod]
        public void TestPostAuthentication_ShouldReturnSuccessFalse()
        {

            var testAuthentication = new TestAuthentication();
            var credentials = new Credentials()
            {
                Username = "manu",
                Password = "test"
            };

            var authenticationController = new AuthenticationController(testAuthentication);

            var result = authenticationController.PostAuthentication(credentials) as OkNegotiatedContentResult<AuthenticationResponse>;
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.Content.Success);
            Assert.AreEqual("Username or password is incorrect", result.Content.Message);
        }
    }
}
