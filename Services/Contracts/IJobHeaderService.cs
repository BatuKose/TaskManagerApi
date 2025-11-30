using Entites.Data_Transfer_object.JobHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IJobHeaderService
    {
        Task<CreateJobHeaderDTO>InsertJobInsertJobHeader(CreateJobHeaderDTO jobHeaderDTO);
    }
}
