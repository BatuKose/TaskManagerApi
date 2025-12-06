using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.DetayStatus;

namespace Entites.Data_Transfer_object.JobDetail
{
    public class InsertJobDetailDTO
    {
        [Required(ErrorMessage ="İş başlık bilgisi olmak zorunda")]
        public int HeaderId { get; set; }
        [Required(ErrorMessage ="Yapılan iş bilgileri doldurulmalıdır")]
        [MinLength(10,ErrorMessage ="minimum 10 karakter olmak zorunda")]
        [MaxLength(254,ErrorMessage ="maximum 254 karakter olmak zorunda")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Kullanıcı bilgisi olmak zorunda")]
        public int userId { get; set; }
        [Required(ErrorMessage = "Durum bilgisi olmak zorunda")]
        public JobDetayStatus jobDetayStatus { get; set; }
    }
}
