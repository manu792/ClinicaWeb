using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class ClinicaDatabaseInitializer : DropCreateDatabaseIfModelChanges<ClinicaContext>
    {
        protected override void Seed(ClinicaContext context)
        {
            base.Seed(context);
            GetPacientes().ForEach(p => context.Pacientes.Add(p));
            GetTratamientos().ForEach(t => context.Tratamientos.Add(t));
        }

        private List<Paciente> GetPacientes()
        {
            var pacientes = new List<Paciente> {
                new Paciente
                {
                    PacienteId = "115190794",
                    Nombre = "Manuel Roman",
                    Edad = 24,
                    Contacto = "87130532",
                    FechaUltimaVisita = DateTime.Now,
                    FechaProximaVisita = DateTime.Now.AddMonths(1)
                },
                new Paciente
                {
                    PacienteId = "125964792",
                    Nombre = "Algeris Cabrera",
                    Edad = 21,
                    Contacto = "61690609",
                    FechaUltimaVisita = DateTime.Now.AddMonths(-1),
                    FechaProximaVisita = DateTime.Now.AddMonths(1)
                },
                new Paciente
                {
                    PacienteId = "322984350",
                    Nombre = "Mauricio Vargas",
                    Edad = 23,
                    Contacto = "88966923",
                    FechaUltimaVisita = DateTime.Now.AddMonths(-1),
                    FechaProximaVisita = DateTime.Now.AddMonths(2)
                }
            };

            return pacientes;
        }

        private List<Tratamiento> GetTratamientos()
        {
            var tratamientos = new List<Tratamiento> {
                new Tratamiento
                {
                    PacienteId = "115190794",
                    FechaInicio = DateTime.Now,
                    FechaConclusion = DateTime.Now.AddDays(8),
                    Costo = 25000,
                    Detalle = "Medicamento basico para control de asma"
                },
                new Tratamiento
                {
                    PacienteId = "115190794",
                    FechaInicio = DateTime.Now.AddDays(-5),
                    Costo = 30000,
                    Detalle = "Medicamentos para alergia"
                },
                new Tratamiento
                {
                    PacienteId = "125964792",
                    FechaInicio = DateTime.Now,
                    FechaConclusion = DateTime.Now.AddMonths(9),
                    Costo = 150000,
                    Detalle = "Control prenatal"
                },
                new Tratamiento
                {
                    PacienteId = "322984350",
                    FechaInicio = DateTime.Now.AddMonths(-1),
                    Costo = 45000,
                    Detalle = "Inyeccion para N1H1"
                },
                new Tratamiento
                {
                    PacienteId = "322984350",
                    FechaInicio = DateTime.Now,
                    FechaConclusion = DateTime.Now.AddMonths(1),
                    Costo = 15000,
                    Detalle = "Pastillas para migraña"
                }
            };

            return tratamientos;
        }
    }
}