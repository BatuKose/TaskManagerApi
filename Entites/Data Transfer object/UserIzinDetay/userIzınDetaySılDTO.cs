using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.UserIzinDetay
{
    public class userIzınDetaySılDTO
    {
        [Required(ErrorMessage ="silinecek izin bilgisi girilmesi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir izin ID giriniz")]
        public int IzınId { get; set; }
    }
}
