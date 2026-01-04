using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object
{
    public class UserIzınDto
    {
        [Required(ErrorMessage = "Kullanıcı bilgileri boş olamaz")]
        public int userId { get; set; }
        public int HakedilenIzın { get; set; }
    }
}
