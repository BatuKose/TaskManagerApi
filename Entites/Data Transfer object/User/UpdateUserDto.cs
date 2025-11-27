using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.User
{
    public class UpdateUserDto
    {
        //[Required(ErrorMessage = "kullanıcı id bilgisi boş olamaz.")]
        //public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı adı 5 karakterden kısa olamaz")]
        public string userName { get; set; }
        [Required(ErrorMessage = "E-posta boş olamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifresi boş olamaz")]
        [MinLength(5, ErrorMessage = "Kullanıcı şifresi 5 karakterden kısa olamaz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "kullanıcı rolü seçilmelidir.")]
        public int RoleId { get; set; }
    }
}
