using Entites.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class jobDetailRepository: IjobDetailRepository
    {
        protected readonly RepositoryContext _Context;
        public jobDetailRepository(RepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

        public async Task<JobDetail> InsertJobDetailAsync(JobDetail jobDetail)
        {
            _Context.jobDetail.Add(jobDetail);
            await _Context.SaveChangesAsync();
            return jobDetail;
        }
    }
}
