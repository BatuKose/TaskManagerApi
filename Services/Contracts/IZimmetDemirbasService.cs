using Entites.Data_Transfer_object.ZimmetDemirbas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IZimmetDemirbasService
    {
        Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<UpdateCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO,int id);
     
    }
}
