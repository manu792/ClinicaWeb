using ClinicaWeb.Autofac;
using ClinicaWeb.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

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