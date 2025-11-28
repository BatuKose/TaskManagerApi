using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.Role
{
    public class InsertRoleDTO
    {
        [Required(ErrorMessage ="Role adı boş bırakılamaz")]
        [MinLength(3,ErrorMessage ="role adı üç karakterden küçük olamaz")]
        public string RoleName { get; set; }
    }
}
