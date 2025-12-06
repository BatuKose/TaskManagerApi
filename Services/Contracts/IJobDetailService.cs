using Entites.Data_Transfer_object.JobDetail;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IJobDetailService
    {
        Task<InsertJobDetailDTO> InsertJobDetailAsync(InsertJobDetailDTO detailDTO);
       
    }
}
