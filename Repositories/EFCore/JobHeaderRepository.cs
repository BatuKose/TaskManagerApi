using Entites.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class JobHeaderRepository : IJobHeaderRepository
    {
        protected readonly RepositoryContext _Context;
        public JobHeaderRepository(RepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

        public async Task<JobHeader> InsertJobHeader(JobHeader jobHeader)
        {
            _Context.jobHeaders.Add(jobHeader);
            await _Context.SaveChangesAsync();
            return jobHeader;
        }
    }
}
