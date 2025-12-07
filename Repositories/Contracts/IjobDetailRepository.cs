using Entites.Data_Transfer_object.JobDetail;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IjobDetailRepository
    {
        Task<JobDetail>InsertJobDetailAsync(JobDetail jobDetail);
        Task<JobDetail> DeleteJobDetailAsync(JobDetail jobDetail);
        Task<JobDetail> GetJobDetailByIdAsync(int id);
        IQueryable<JobHeader> IsJobDone(int id);
        IQueryable<JobDetayStatusWithHeaderDTO> JobStatusWithHedaer(int id);
        public bool HeaderVarmi(int id);
        public bool DetailVarmi(int id);
    }
}
