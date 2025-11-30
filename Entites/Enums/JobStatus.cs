using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Enums
{
    public class JobStatusEnum
    {
        public enum JobStatus
        {
            Bekleniyor = 0,
            Karşılandı = 1,
            Done = 2,
            iptal = 3,
            Cezalı=4    
        }
    }
}
