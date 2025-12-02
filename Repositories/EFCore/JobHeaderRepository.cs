using Entites.Data_Transfer_object.JobHeader;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> FındAdminOrManagerWorkersAsync(int id)
        {
            var query =
                from u in _Context.users
                join r in _Context.roles on u.RoleId equals r.Id
                where u.Id == id && (r.Id == 1 || r.Id == 2)
                select u;

            return await query.AnyAsync();
        }

        public async Task<bool> FındWorkersAsync(int id)
        {
            var query =
                from u in _Context.users
                join r in _Context.roles on u.RoleId equals r.Id
                where u.Id==id && (r.Id!=1 || r.Id!=2)
                select u;
            return await query.AnyAsync();
        }

        public async Task<JobHeader> InsertJobHeader(JobHeader jobHeader)
        {
            _Context.jobHeaders.Add(jobHeader);
            await _Context.SaveChangesAsync();
            return jobHeader;
        }

        public async Task<bool> isUserActive(int id)
        {
            var query= await _Context.users.AnyAsync(x=>x.Id == id && x.aktifMi==true);
            return query;
        }
    }
}
