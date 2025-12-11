using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı adı 5 karakterden kısa olamaz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifresi boş olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı şifresi 5 karakterden kısa olamaz")]
        public string PassWord { get; set; }
    }
}
