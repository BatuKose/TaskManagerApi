using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string ipAdress { get; set; }
        public DateTime girisTarihi { get; set; }
        public bool isSuccess { get; set; }

    }
}
