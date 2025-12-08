using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.DetayStatus;

namespace Entites.Models
{
    public class JobDetail
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string Detail { get; set; }
        public int userId { get; set; }
        public JobDetayStatus jobDetayStatus { get; set; }
        public DateTime JobFinishTime {  get; set; }
    }
}
