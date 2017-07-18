using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.Entity;
using ClinicaWeb.Models;
using ClinicaWeb.Autofac;

namespace ClinicaWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Initialize the DB with some data
            Database.SetInitializer(new ClinicaDatabaseInitializer());

            //Registering controllers and setting dependency resolver with Autofac
            Bootstrapper.Run();
        }
    }
}