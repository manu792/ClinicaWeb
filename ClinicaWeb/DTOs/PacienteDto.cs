using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWeb.DTOs
{
    public class PacienteDto
    {
        public string PacienteId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Contacto { get; set; }
        public DateTime FechaUltimaVisita { get; set; }
        public DateTime FechaProximaVisita { get; set; }
        public IEnumerable<TratamientoDto> Tratamientos { get; set; }
    }
}