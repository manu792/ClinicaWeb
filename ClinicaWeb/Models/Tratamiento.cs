using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class Tratamiento
    {
        public int Id { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        public Nullable<DateTime> FechaConclusion { get; set; }
        [Required]
        public double Costo { get; set; }
        [Required]
        public string Detalle { get; set; }
        [Required]
        public string PacienteId { get; set; }
        
        
        // Navigation property
        public virtual Paciente Paciente { get; set; }
    }
}