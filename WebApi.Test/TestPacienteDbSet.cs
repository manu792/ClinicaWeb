using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    class TestPacienteDbSet : TestDbSet<Paciente>
    {
        public override Paciente Find(params object[] keyValues)
        {
            return this.SingleOrDefault(paciente => paciente.PacienteId == (string)keyValues.Single());
        }
    }
}
