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
    }
}
