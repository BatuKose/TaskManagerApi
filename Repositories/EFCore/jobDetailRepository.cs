using Entites.Data_Transfer_object.JobDetail;
using Entites.Enums;
using Entites.Models;
using Entites.View;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Serilog.Formatting.Json;
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

        public async Task<JobDetail> DeleteJobDetailAsync(JobDetail jobDetail)
        {
            _Context.jobDetail.Remove(jobDetail);
            await _Context.SaveChangesAsync();
            return jobDetail;
        }

        public bool DetailVarmi(int id)
        {
            var result = _Context.jobDetail.Any(x => x.HeaderId==id);
            return result;
        }

        public List<CezalıIslerView> GetCezalıİsler()
        {

            var data = _Context.CezaliIsler
                .FromSqlRaw(@"
                WITH CTE AS (
                SELECT 
               
                jh.Title AS JobHeaderName,
                jh.Deadline,
                
                jd.Detail AS JobDetailName,
                jd.JobFinishTime AS FinishedTime,
                ROW_NUMBER() OVER (PARTITION BY jh.Id ORDER BY jd.JobFinishTime DESC) AS rn
                FROM jobDetail jd
                LEFT JOIN jobHeaders jh ON jd.HeaderId = jh.Id
                )
                SELECT *
                FROM CTE
                WHERE rn = 1 AND FinishedTime > Deadline
                ").ToList();
                return data;
         }

        public async Task<JobDetail> GetJobDetailByIdAsync(int id)
        {
            var result = await _Context.jobDetail.Where(x => x.Id == id).SingleOrDefaultAsync();
            return result;
        }

        public bool HeaderVarmi(int id)
        {
            var result = _Context.jobHeaders.Any(x => x.Id==id);
            return result;
        }

        public async Task<JobDetail> InsertJobDetailAsync(JobDetail jobDetail)
        {
            _Context.jobDetail.Add(jobDetail);
            await _Context.SaveChangesAsync();
            return jobDetail;
        }

        public IQueryable<JobHeader> IsJobDone(int id)
        {
            var query = from j in _Context.jobHeaders
                        join jd in _Context.jobDetail on j.Id equals jd.HeaderId
                        where jd.Id == id && j.Status == JobStatusEnum.JobStatus.Done
                        select j;
            return query;
        }

        public IQueryable<JobDetayStatusWithHeaderDTO> JobStatusWithHedaer(int id)
        {
            var query = from j in _Context.jobHeaders
                        join jd in _Context.jobDetail on j.Id equals jd.HeaderId
                        join u in _Context.users on j.AssignedUserId equals u.Id
                        join u1 in _Context.users on j.ManagerId equals u1.Id
                        join r in _Context.roles on u.RoleId equals r.Id
                        join r1 in _Context.roles on u1.RoleId equals r1.Id
                        where j.Id == id 

                        select new JobDetayStatusWithHeaderDTO()
                        {
                            JobHeaderId=j.Id,
                            JobDetailId =jd.Id,
                            JobHeaderName=j.Title,
                            WorkCreateTıme=j.CreatedDate,
                            Deadline=j.Deadline,
                            ManagerUserName=u1.UserName,
                            ManagerRole=r1.RoleName,
                            JobDetayName=jd.Detail,
                            WorkerUserName=u.UserName,
                            workerRole=r.RoleName,
                            jobHeaderStatus=j.Status,
                            jobDetayStatus=jd.jobDetayStatus
                        };
            return query;
        }
        public async Task<List<JobDetayStatusWithHeaderDTO>> BütünisleriGetir(bool? active)
        {
            var baseQuery = from j in _Context.jobHeaders
                            join jd in _Context.jobDetail on j.Id equals jd.HeaderId
                            join u in _Context.users on j.AssignedUserId equals u.Id
                            join u1 in _Context.users on j.ManagerId equals u1.Id
                            join r in _Context.roles on u.RoleId equals r.Id
                            join r1 in _Context.roles on u1.RoleId equals r1.Id
                            select new { j, jd, u, u1, r, r1 };


            if (active == true)
            {
                baseQuery = baseQuery.Where(x => x.j.Status != JobStatusEnum.JobStatus.Done);
            }
            else if (active == false)
            {
                baseQuery = baseQuery.Where(x => x.j.Status == JobStatusEnum.JobStatus.Done);
            }

            var result = await baseQuery.Select(x => new JobDetayStatusWithHeaderDTO
            {
                JobHeaderId = x.j.Id,
                JobHeaderName = x.j.Title,
                WorkCreateTıme = x.j.CreatedDate,
                Deadline = x.j.Deadline,
                ManagerUserName = x.u1.UserName,
                ManagerRole = x.r1.RoleName,
                JobDetayName = x.jd.Detail,
                WorkerUserName = x.u.UserName,
                workerRole = x.r.RoleName,
                jobHeaderStatus = x.j.Status,
                jobDetayStatus = x.jd.jobDetayStatus,
                JobFinishedTime = x.jd.JobFinishTime
            }).ToListAsync();

            return result;
        }
    }
}
