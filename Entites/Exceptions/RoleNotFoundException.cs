using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class RoleNotFoundException :CustomException
    {
        public RoleNotFoundException() : base("Rol bilgileri bulunamadı.", (int)HttpStatusCode.NotFound) { }
    }
}
