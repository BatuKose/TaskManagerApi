using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Data_Transfer_object.ZimmetDemirbas
{
    public class SelectZimmetDetailListDTO
    {
        public int dosyaId { get; set; }
        public string ZimmetKisiAd { get; set; }
        public string ZimmetKisiEmail { get; set; }
        
        public string KisiRol { get; set; }
        public string UrunAd { get; set; }
        public string Model { get; set; }
        public string UrunKategoriAd { get; set; }
        public int ZimmetMiktar { get; set; }
        public DateTime ZimmetTarih { get; set; }

    }
}
