using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class ClinicaContext : DbContext, IContext
    {
        public ClinicaContext() : base("ClinicaDB")
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        public void MarkAsModified<T>(T item) where T : class
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}