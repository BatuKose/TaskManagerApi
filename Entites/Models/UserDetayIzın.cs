using Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.IzınDetay;

namespace Entites.Models
{
    public class UserDetayIzın
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public IzınDetayEnum IzınDetay  { get; set; }
        public bool YoneticiOnay { get; set; }
    }
}
