using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.JobStatusEnum;

namespace Entites.Data_Transfer_object.JobHeader
{
    public class CreateJobHeaderDTO
    {
        public class CreateJobDto
        {
            public string Title { get; set; }
            public int ManagerId { get; set; }
            public int AssignedUserId { get; set; }
            public JobStatus Status { get; set; } = JobStatus.Bekleniyor;
            public DateTime Deadline { get; set; }
        }

    }
}
