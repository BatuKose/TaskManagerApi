using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.JobStatusEnum;

namespace Entites.Models
{
    public class JobHeader
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ManagerId { get; set; }
        public int AssignedUserId { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Bekleniyor;
        public DateTime Deadline { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
