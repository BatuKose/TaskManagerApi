using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.JobStatusEnum;

namespace Entites.Data_Transfer_object.JobHeader
{
 
        public class CreateJobHeaderDTO
        {
            [Required(ErrorMessage ="Yapılması istenen işin bilgileri boş geçilemez")]
            [MinLength(10,ErrorMessage = "Yapılması istenen işin bilgileri minimum 10 karakter olmalıdır")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Yönetici bilgileri boş geçilemez")]
            public int ManagerId { get; set; }
          //  [Required(ErrorMessage = "çalışan bilgileri boş geçilemez")]
            public int AssignedUserId { get; set; }
            [Required(ErrorMessage = "görev bitiş zaman bilgileri boş geçilemez")]
            public DateTime Deadline { get; set; }
        }

    
}
