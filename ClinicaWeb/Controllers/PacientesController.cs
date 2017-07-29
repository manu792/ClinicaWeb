using ClinicaWeb.DTOs;
using ClinicaWeb.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ClinicaWeb.Controllers
{
    public class PacientesController : ApiController
    {
        private IContext DbContext { get; set; }
        public PacientesController(IContext dbContext)
        {
            DbContext = dbContext;
        }

        // GET: api/Pacientes
        public IEnumerable<PacienteDto> GetPacientes()
        {
            return DbContext.Pacientes.Select(p => new PacienteDto
            {
                PacienteId = p.PacienteId,
                Nombre = p.Nombre,
                Edad = p.Edad,
                Contacto = p.Contacto,
                FechaUltimaVisita = p.FechaUltimaVisita,
                FechaProximaVisita = p.FechaProximaVisita
            });
        }

        // GET: api/Pacientes/5
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult GetPaciente(string id)
        {
            Paciente paciente = DbContext.Pacientes.Where(p => p.PacienteId == id).Include(t => t.Tratamientos).SingleOrDefault();
            IEnumerable<TratamientoDto> tratamientosDto = ConvertTratamientos(paciente.Tratamientos);
            if (paciente == null)
            {
                return NotFound();
            }

            PacienteDto pacienteDto = new PacienteDto
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                Edad = paciente.Edad,
                Contacto = paciente.Contacto,
                FechaUltimaVisita = paciente.FechaUltimaVisita,
                FechaProximaVisita = paciente.FechaProximaVisita,
                Tratamientos = tratamientosDto
            };

            return Ok(pacienteDto);
        }

        // PUT: api/Pacientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaciente(string id, Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paciente.PacienteId)
            {
                return BadRequest();
            }

            DbContext.MarkAsModified(paciente);

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult PostPaciente(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DbContext.Pacientes.Add(paciente);

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PacienteExists(paciente.PacienteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = paciente.PacienteId }, paciente);
        }

        // DELETE: api/Pacientes/5
        [ResponseType(typeof(Paciente))]
        public IHttpActionResult DeletePaciente(string id)
        {
            Paciente paciente = DbContext.Pacientes.Find(id);
            if (paciente == null)
            {
                return NotFound();
            }

            DbContext.Pacientes.Remove(paciente);
            DbContext.SaveChanges();

            return Ok(paciente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacienteExists(string id)
        {
            return DbContext.Pacientes.Count(e => e.PacienteId == id) > 0;
        }

        private IEnumerable<TratamientoDto> ConvertTratamientos(IEnumerable<Tratamiento> tratamientos)
        {
            List<TratamientoDto> tratamientosDto;

            if (tratamientos == null)
                return null;

            tratamientosDto = new List<TratamientoDto>();
            foreach (Tratamiento tratamiento in tratamientos)
            {
                tratamientosDto.Add(new TratamientoDto
                {
                    Id = tratamiento.Id,
                    Costo = tratamiento.Costo,
                    Detalle = tratamiento.Detalle,
                    FechaConclusion = tratamiento.FechaConclusion,
                    FechaInicio = tratamiento.FechaInicio,
                    PacienteId = tratamiento.PacienteId
                });
            }

            return tratamientosDto;
        }
    }
}
