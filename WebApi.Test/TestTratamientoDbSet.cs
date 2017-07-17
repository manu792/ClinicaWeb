using ClinicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    class TestTratamientoDbSet : TestDbSet<Tratamiento>
    {
        public override Tratamiento Find(params object[] keyValues)
        {
            return this.SingleOrDefault(tratamiento => tratamiento.Id == (int)keyValues.Single());
        }
    }
}
