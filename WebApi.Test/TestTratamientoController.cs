using ClinicaWeb.Controllers;
using ClinicaWeb.DTOs;
using ClinicaWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace WebApi.Test
{
    [TestClass]
    public class TestTratamientoController
    {
        [TestMethod]
        public void PostTratamiento_ShouldReturnSameTratamiento()
        {
            var controller = new TratamientosController(new TestClinicaContext());

            var item = GetDemoTratamiento();

            var result =
                controller.PostTratamiento(item) as CreatedAtRouteNegotiatedContentResult<Tratamiento>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Detalle, item.Detalle);
        }

        [TestMethod]
        public void PutTratamiento_ShouldReturnStatusCode()
        {
            var controller = new TratamientosController(new TestClinicaContext());

            var item = GetDemoTratamiento();

            var result = controller.PutTratamiento(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutTratamiento_ShouldFail_WhenDifferentId()
        {
            var controller = new TratamientosController(new TestClinicaContext());

            var badresult = controller.PutTratamiento(3, GetDemoTratamiento());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetTratamiento_ShouldReturnTratamientoWithSameId()
        {
            var context = new TestClinicaContext();
            context.Tratamientos.Add(GetDemoTratamiento());

            var controller = new TratamientosController(context);
            var result = controller.GetTratamiento(1) as OkNegotiatedContentResult<TratamientoDto>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
        }

        [TestMethod]
        public void GetTratamiento_ShouldReturnAllTratamientos()
        {
            var context = new TestClinicaContext();
            context.Tratamientos.Add(new Tratamiento { Id = 2, Costo = 2500, Detalle = "Prueba", FechaConclusion = DateTime.Now.AddMonths(3), FechaInicio = DateTime.Now, PacienteId = "12563215" });
            context.Tratamientos.Add(new Tratamiento { Id = 3, Costo = 2500, Detalle = "Prueba", FechaConclusion = DateTime.Now.AddMonths(3), FechaInicio = DateTime.Now, PacienteId = "56548789" });
            context.Tratamientos.Add(new Tratamiento { Id = 4, Costo = 2500, Detalle = "Prueba", FechaConclusion = DateTime.Now.AddMonths(3), FechaInicio = DateTime.Now, PacienteId = "55682301" });

            var controller = new TratamientosController(context);
            var result = controller.GetTratamientos();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.LongCount());
        }

        [TestMethod]
        public void DeleteTratamiento_ShouldReturnOK()
        {
            var context = new TestClinicaContext();
            var item = GetDemoTratamiento();
            context.Tratamientos.Add(item);

            var controller = new TratamientosController(context);
            var result = controller.DeleteTratamiento(1) as OkNegotiatedContentResult<Tratamiento>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        private Tratamiento GetDemoTratamiento()
        {
            return new Tratamiento()
            {
                 Id = 1,
                 Costo = 2500,
                 Detalle = "Prueba",
                 FechaConclusion = DateTime.Now.AddMonths(1),
                 FechaInicio = DateTime.Now,
                 PacienteId = "115190794"
            };
        }

    }
}
