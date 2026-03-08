using Entites.Data_Transfer_object.JobHeader;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IJobHeaderRepository
    {
        Task<JobHeader> InsertJobHeader(JobHeader jobHeader);
        Task<bool>FındAdminOrManagerWorkersAsync(int id);
        Task<bool> FındWorkersAsync(int id);
        Task<bool>isUserActive(int id);
        Task<JobHeader>DeleteHeaderJobAsync(JobHeader jobHeader);
        Task<JobHeader>FindJobHeaderAsync(int id);
        SelectJobHeaderDTO SelectJobHeader(int id);
       Task< JobHeader> IsKarsila(JobHeader job);
       Task<JobHeader> FindJobWithUser(int jobid, int userId);
       Task<JobHeader> SelectJobHeaderById(int id);
       Task<JobHeader> UpdateJobHeader(JobHeader jobHeader);
       Task<List<SelectJobHeaderDTO>> SelectJobHeaderAll(bool? bitmisMi);
    }
}
