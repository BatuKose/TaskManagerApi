using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.ZimmetDemirbas
{
    public class CreateProductDto
    {
        [Required(ErrorMessage ="kategori id girilmesi zorunludur")]
        [Range(1,int.MaxValue,ErrorMessage ="kategori id 1 den büyük olmalıdır")]
        public int categoryId { get; set; }
        [Required(ErrorMessage = "isim girilmesi zorunludur")]
        [MinLength(5, ErrorMessage = "minimum 5 karakter zorunlu")]
        public string name { get; set; }
        [MinLength(5, ErrorMessage = "minimum 5 karakter zorunlu")]
        [Required(ErrorMessage = "marka zorunludur")]
        public string brand { get; set; }
        [MinLength(5, ErrorMessage = "minimum 5 karakter zorunlu")]
        [Required(ErrorMessage = "model girilmesi zorunludur")]
        public string model { get; set; }
        
        public string description { get; set; }
        [Required(ErrorMessage = "Miktar bilgisi girilmesi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "miktar bilgisi birden den büyük olmalıdır")]
        public int unit { get; set; }

    }
}
