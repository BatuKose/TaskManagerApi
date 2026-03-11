using Entites.Data_Transfer_object.JobDetail;
using Entites.Data_Transfer_object.JobHeader;
using Entites.Enums;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
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

        public async Task<JobHeader> DeleteHeaderJobAsync(JobHeader jobHeader)
        {
           var query=  _Context.jobHeaders.Remove(jobHeader);
            await _Context.SaveChangesAsync();
            return jobHeader;
        }

        public async Task<JobHeader> FindJobHeaderAsync(int id)
        {
           var query= await _Context.jobHeaders.SingleOrDefaultAsync(x=>x.Id==id);
            return query;
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
        public async Task<JobHeader>FindJobWithUser(int jobid,int  userId)
        {
            return await _Context.jobHeaders
             .SingleOrDefaultAsync(x => x.Id == jobid && x.AssignedUserId == userId);
    
        }
        public async Task<JobHeader> IsKarsila(JobHeader job)
        {
            _Context.jobHeaders.Update(job);
            await _Context.SaveChangesAsync();
            return job;
        }


        public async Task<bool> isUserActive(int id)
        {
            var query= await _Context.users.AnyAsync(x=>x.Id == id && x.aktifMi==true);
            return query;
        }

        public SelectJobHeaderDTO SelectJobHeader(int id)
        {
            var query  =
                from j in _Context.jobHeaders
                join u in _Context.users on j.AssignedUserId equals u.Id
                join u2 in _Context.users on j.ManagerId equals u2.Id
                join r in _Context.roles on u.RoleId equals r.Id
                join r2 in _Context.roles on u2.Id equals r2.Id
                where j.Id==id
                select new SelectJobHeaderDTO 
                {
                    Title= j.Title,
                    ManagerName=u2.UserName,
                    AssignedUser=u.UserName,
                    Status=j.Status,
                    Deadline=j.Deadline,
                    CreatedDate=j.CreatedDate,
                    userRoleName=r.RoleName,
                    managerRoleName=r2.RoleName
                };
            return  query.SingleOrDefault();
        }
        public async Task<List<SelectJobHeaderDTO>> KendiİsBasliklarim(int id)
        {
            var query =
                from j in _Context.jobHeaders
                join u in _Context.users on j.AssignedUserId equals u.Id
                join u2 in _Context.users on j.ManagerId equals u2.Id
                join r in _Context.roles on u.RoleId equals r.Id
                join r2 in _Context.roles on u2.RoleId equals r2.Id
                where j.AssignedUserId == id
                select new SelectJobHeaderDTO
                {
                    DosyaId = j.Id,
                    Title = j.Title,
                    ManagerName = u2.UserName,
                    AssignedUser = u.UserName,
                    Status = j.Status,
                    Deadline = j.Deadline,
                    CreatedDate = j.CreatedDate,
                    userRoleName = r.RoleName,
                    managerRoleName = r2.RoleName
                };

            return await query.ToListAsync(); 
        }
        public async Task<List<SelectJobHeaderDTO>> SelectJobHeaderAll(bool? bitmisMi)
        {
            var baseQuery =
                from j in _Context.jobHeaders
                join u2 in _Context.users on j.ManagerId equals u2.Id
                join r2 in _Context.roles on u2.RoleId equals r2.Id
                from u in _Context.users                                   
                    .Where(u => u.Id == j.AssignedUserId)
                    .DefaultIfEmpty()
                from r in _Context.roles                                    
                    .Where(r => r.Id == (u != null ? u.RoleId : 0))
                    .DefaultIfEmpty()
                select new { j, u, u2, r, r2 };

            if (bitmisMi == true)
                baseQuery = baseQuery.Where(x => x.j.Status == JobStatusEnum.JobStatus.Done);
            else
                baseQuery = baseQuery.Where(x => x.j.Status != JobStatusEnum.JobStatus.Done);

            return await baseQuery.Select(x => new SelectJobHeaderDTO
            {
                DosyaId = x.j.Id,
                Title = x.j.Title,
                ManagerName = x.u2.UserName,
                AssignedUser = x.u != null ? x.u.UserName : "Atanmadı",   
                Status = x.j.Status,
                Deadline = x.j.Deadline,
                CreatedDate = x.j.CreatedDate,
                userRoleName = x.r != null ? x.r.RoleName : "Rol Yok",   
                managerRoleName = x.r2.RoleName
            }).ToListAsync();
        }

        public async Task<JobHeader> UpdateJobHeader(JobHeader jobHeader)
        {
            _Context.jobHeaders.Update(jobHeader);
            await _Context.SaveChangesAsync();
            return jobHeader;
        }

        public async Task<JobHeader> SelectJobHeaderById(int id)
        {
            return await _Context.jobHeaders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
