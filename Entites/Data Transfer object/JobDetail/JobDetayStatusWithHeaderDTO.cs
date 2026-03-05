using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.DetayStatus;
using static Entites.Enums.JobStatusEnum;

namespace Entites.Data_Transfer_object.JobDetail
{
    public class JobDetayStatusWithHeaderDTO
    {
        public int JobHeaderId { get; set; }
        public string JobHeaderName { get; set; }
        public string JobDetayName { get; set; }
        public string ManagerUserName { get; set; }
        public string WorkerUserName { get; set; }
        public string workerRole { get; set; }
        public string ManagerRole { get; set; }
        public DateTime WorkCreateTıme { get; set; }
        public DateTime Deadline { get; set; }
        public JobDetayStatus jobDetayStatus { get; set; }
        public JobStatus jobHeaderStatus { get; set; }
        public DateTime JobFinishedTime { get; set; }
    }
}
