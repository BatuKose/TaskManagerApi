using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.IzınDetay;

namespace Entites.Data_Transfer_object.UserIzinDetay
{
    public class UserIzinDetayEkleDTO
    {
        [Required(ErrorMessage = "Kullanıcı bilgileri boş olamaz")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "izin başlangıç bilgileri boş olamaz")]
        public DateTime BaslangicTarihi { get; set; }
        [Required(ErrorMessage = "izin bitiş bilgileri boş olamaz")]
        public DateTime BitisTarihi { get; set; }
        [Required(ErrorMessage = "izin tür bilgileri boş olamaz")]
        public IzınDetayEnum IzınDetay { get; set; }
        public bool YoneticiOnay { get; set; } = false;
    }
}
