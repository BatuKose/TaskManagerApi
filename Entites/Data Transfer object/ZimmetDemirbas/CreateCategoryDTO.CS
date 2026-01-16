using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.ZimmetDemirbas
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage ="Kategori adı girilmesi zorunludur.")]
        [MinLength(5,ErrorMessage ="Minimum beş karekter girmek zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kategori adı girilmesi zorunludur.")]
        [MinLength(5, ErrorMessage = "Minimum beş karekter girmek zorunludur.")]
        public string Description { get; set; }
       // public bool isActive { get; set; } = true;
    }
}
