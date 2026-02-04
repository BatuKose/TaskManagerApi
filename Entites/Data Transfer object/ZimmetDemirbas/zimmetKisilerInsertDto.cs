using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.ZimmetDemirbas
{
    public class zimmetKisilerInsertDto
    {
        [Required (ErrorMessage = "Kullanıcı Id alanı boş geçilemez.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "ürün Id alanı boş geçilemez.")]
        public int ProcudtId { get; set; }
        [Required(ErrorMessage = " miktar alanı boş geçilemez.")]
        public int Unit { get; set; }
    }
}
