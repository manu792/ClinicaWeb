using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaWeb.Models
{
    //Interface for authentication so we can change authentication data source in the future by implementing this interface
    public interface IAuthentication
    {
        bool Authenticate(Credentials credentials);
    }
}
