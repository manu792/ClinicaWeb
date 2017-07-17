using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWeb.DTOs
{
    public class TratamientoDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public Nullable<DateTime> FechaConclusion { get; set; }
        public double Costo { get; set; }
        public string Detalle { get; set; }
        public string PacienteId { get; set; }
    }
}