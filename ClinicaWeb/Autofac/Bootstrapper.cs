using Autofac;
using Autofac.Integration.WebApi;
using ClinicaWeb.Controllers;
using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace ClinicaWeb.Autofac
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPI();
        }

        private static void SetAutofacWebAPI()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register specific implementations
            builder.RegisterType<ClinicaContext>().As<IContext>().InstancePerRequest();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}