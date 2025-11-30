using Entites.Data_Transfer_object.JobHeader;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobHeaderManager : IJobHeaderService
    {
        private readonly IRepositoryManager _repositoryManager;

        public JobHeaderManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<CreateJobHeaderDTO> InsertJobInsertJobHeader(CreateJobHeaderDTO jobHeaderDTO)
        {
            throw new NotImplementedException();
        }
    }

}
