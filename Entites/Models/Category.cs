using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }= true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
