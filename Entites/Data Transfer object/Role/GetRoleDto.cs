using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.Role
{
    public class GetRoleDto
    {
        [Required(ErrorMessage ="rol id bilgisi boş olamaz")]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
