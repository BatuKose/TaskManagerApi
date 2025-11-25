using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class UserNotFoundException : CustomException
    {
        public UserNotFoundException() : base("Kullanıcı bilgileri bulunamadı.", (int)HttpStatusCode.NotFound){}
    }
}
