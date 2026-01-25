using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class Product
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string description { get; set; }
        public DateTime crateAt { get; set; } = DateTime.Now;
        public bool isActive { get; set; } = true;
        public int unit { get; set; }
    }
}
