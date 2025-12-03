using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.JobStatusEnum;

namespace Entites.Data_Transfer_object.JobHeader
{
    public class SelectJobHeaderDTO
    {
        public string Title { get; set; }
        public string ManagerName { get; set; }
        public string AssignedUser { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Bekleniyor;
        public DateTime Deadline { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string userRoleName { get; set; }
        public string managerRoleName { get; set; }


    }
}
