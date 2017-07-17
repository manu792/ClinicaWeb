using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaWeb.Models
{
    public interface IContext : IDisposable
    {
        DbSet<Paciente> Pacientes { get; set; }
        DbSet<Tratamiento> Tratamientos { get; set; }
        int SaveChanges();
        void MarkAsModified<T>(T item) where T : class;
    }
}
