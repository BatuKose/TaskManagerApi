using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.User
{
    public class GetUserDto
    {
        [Required(ErrorMessage = "Kullanıcı id boş olamaz")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        public string userName { get; set; }
        [Required(ErrorMessage = "E-posta boş olamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifresi boş olamaz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Kullanıcı rol bilgisi boş olamaz")]
        public int roleId { get; set; }

    }
}
