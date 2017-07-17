using ClinicaWeb.DTOs;
using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ClinicaWeb.Controllers
{
    [Authorize]
    public class TratamientosController : ApiController
    {
        private IContext DbContext { get; set; }
        public TratamientosController(IContext dbContext)
        {
            DbContext = dbContext;
        }

        // GET: api/Tratamientos
        public IEnumerable<TratamientoDto> GetTratamientos()
        {
            return DbContext.Tratamientos.Select(p => new TratamientoDto
            {
                Id = p.Id,
                FechaInicio = p.FechaInicio,
                FechaConclusion = p.FechaConclusion,
                Costo = p.Costo,
                Detalle = p.Detalle,
                PacienteId = p.PacienteId
            });
        }

        // GET: api/Tratamientos/5
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult GetTratamiento(int id)
        {
            Tratamiento tratamiento = DbContext.Tratamientos.Find(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            TratamientoDto tratamientoDto = new TratamientoDto
            {
                Id = tratamiento.Id,
                FechaInicio = tratamiento.FechaInicio,
                FechaConclusion = tratamiento.FechaConclusion,
                Costo = tratamiento.Costo,
                Detalle = tratamiento.Detalle,
                PacienteId = tratamiento.PacienteId
            };

            return Ok(tratamientoDto);
        }

        // PUT: api/Tratamientos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTratamiento(int id, Tratamiento tratamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tratamiento.Id)
            {
                return BadRequest();
            }

            DbContext.MarkAsModified(tratamiento);

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tratamientos
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult PostTratamiento(Tratamiento tratamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DbContext.Tratamientos.Add(tratamiento);
            DbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tratamiento.Id }, tratamiento);
        }

        // DELETE: api/Tratamientos/5
        [ResponseType(typeof(Tratamiento))]
        public IHttpActionResult DeleteTratamiento(int id)
        {
            Tratamiento tratamiento = DbContext.Tratamientos.Find(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            DbContext.Tratamientos.Remove(tratamiento);
            DbContext.SaveChanges();

            return Ok(tratamiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TratamientoExists(int id)
        {
            return DbContext.Tratamientos.Count(e => e.Id == id) > 0;
        }
    }
}
