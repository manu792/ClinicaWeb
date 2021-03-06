﻿using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Models
{
    public class TestClinicaContext : IContext
    {
        public TestClinicaContext()
        {
            Pacientes = new TestPacienteDbSet();
            Tratamientos = new TestTratamientoDbSet();
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        public void Dispose()
        {

        }

        public void MarkAsModified<T>(T item) where T : class
        {

        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
