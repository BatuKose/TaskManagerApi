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
            Bekleniyor = 1,
            Karşılandı = 2,
            Done = 3,
            iptal = 4,
            Cezalı=5    
        }
    }
}
