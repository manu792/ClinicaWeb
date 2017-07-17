using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class Paciente
    {
        [Key]
        public string PacienteId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        public string Contacto { get; set; }
        [Required]
        public DateTime FechaUltimaVisita { get; set; }
        [Required]
        public DateTime FechaProximaVisita { get; set; }

        // Navigation property
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }
    }
}