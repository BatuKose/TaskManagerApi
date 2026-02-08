using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.ZimmetDurumEnums;

namespace Entites.Models
{
    public class ZımmetliKisiler
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProcudtId { get; set; }
        public int Unit { get; set; }
        public DateTime ZimmetAlisTarihi { get; set; } = DateTime.UtcNow;
        public DateTime ZimmetOnayTarihi { get; set; }
        public ZimmetDurum zimmetDurum { get; set; }
    }
}
