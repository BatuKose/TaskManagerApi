using Entites.Data_Transfer_object.JobDetail;
using Entites.Models;
using Entites.View;
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
        Task<JobDetail>DeleteJobDetailAsync(int id);
        public IQueryable<JobDetayStatusWithHeaderDTO> SelectJobDetaiAllDetail(int id);
        public List<CezalıIslerView> GetCezalıİsler();
        Task<List<JobDetayStatusWithHeaderDTO>> BütünİsleriGetirAsync();

    }
}
