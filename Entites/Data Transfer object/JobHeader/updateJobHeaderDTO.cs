using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.JobHeader
{
    public class updateJobHeaderDTO
    {
        [Required(ErrorMessage = "Yapılması istenen işin bilgileri boş geçilemez")]
        [MinLength(10, ErrorMessage = "Yapılması istenen işin bilgileri minimum 10 karakter olmalıdır")]
        public string Title { get; set; }
        [Required(ErrorMessage = "çalışan bilgileri boş geçilemez")]
        public int AssignedUserId { get; set; }

    }
}
