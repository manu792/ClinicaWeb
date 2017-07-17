using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClinicaWeb.Models;
using ClinicaWeb.Controllers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using System.Net;
using ClinicaWeb.DTOs;

namespace WebApi.Test
{
    [TestClass]
    public class TestPacienteController
    {
        [TestMethod]
        public void PostPaciente_ShouldReturnSamePaciente()
        {
            var controller = new PacientesController(new TestClinicaContext());

            var item = GetDemoPaciente();

            var result =
                controller.PostPaciente(item) as CreatedAtRouteNegotiatedContentResult<Paciente>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.PacienteId);
            Assert.AreEqual(result.Content.Nombre, item.Nombre);
        }

        [TestMethod]
        public void PutPaciente_ShouldReturnStatusCode()
        {
            var controller = new PacientesController(new TestClinicaContext());

            var item = GetDemoPaciente();

            var result = controller.PutPaciente(item.PacienteId, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutPaciente_ShouldFail_WhenDifferentPacienteId()
        {
            var controller = new PacientesController(new TestClinicaContext());

            var badresult = controller.PutPaciente("1", GetDemoPaciente());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetPaciente_ShouldReturnPacienteWithSamePacienteId()
        {
            var context = new TestClinicaContext();
            context.Pacientes.Add(GetDemoPaciente());

            var controller = new PacientesController(context);
            var result = controller.GetPaciente("115190794") as OkNegotiatedContentResult<PacienteDto>;

            Assert.IsNotNull(result);
            Assert.AreEqual("115190794", result.Content.PacienteId);
        }

        [TestMethod]
        public void GetPaciente_ShouldReturnAllPacientes()
        {
            var context = new TestClinicaContext();
            context.Pacientes.Add(new Paciente { PacienteId = "10", Nombre = "Fernando", Edad = 20 });
            context.Pacientes.Add(new Paciente { PacienteId = "4", Nombre = "Juan", Edad = 23 });
            context.Pacientes.Add(new Paciente { PacienteId = "200", Nombre = "Pepe", Edad = 40 });

            var controller = new PacientesController(context);
            IEnumerable<PacienteDto> result = controller.GetPacientes();
            
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.LongCount());
        }

        [TestMethod]
        public void DeletePaciente_ShouldReturnOK()
        {
            var context = new TestClinicaContext();
            var item = GetDemoPaciente();
            context.Pacientes.Add(item);

            var controller = new PacientesController(context);
            var result = controller.DeletePaciente("115190794") as OkNegotiatedContentResult<Paciente>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.PacienteId, result.Content.PacienteId);
        }

        private Paciente GetDemoPaciente()
        {
            return new Paciente()
            {
                PacienteId = "115190794",
                Nombre = "Manuel Roman",
                Edad = 24,
                Contacto = "87130532",
                FechaUltimaVisita = DateTime.Now,
                FechaProximaVisita = DateTime.Now.AddMonths(2)
            };
        }
    }
}
