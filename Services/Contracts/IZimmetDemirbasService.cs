using Entites.Data_Transfer_object.ZimmetDemirbas;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.ZimmetDurumEnums;

namespace Services.Contracts
{
    public interface IZimmetDemirbasService
    {
        Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<UpdateCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO,int id);
        Task<CreateProductDto> InsertProductAsync(CreateProductDto dto);
        Task<Category> SoftDeleteCategoryAsync(int id);
        Task<Product> SoftDeleteProductAsync(int id);
        Task<List<UrünBilgileriDTO>> GetProductsWithCategoryAsync();
        Task<zimmetKisilerInsertDto> InsertZimmetKisilerAsync(zimmetKisilerInsertDto dto);
        Task<List<SelectZimmetDetailListDTO>> SelectZimmetDetailsListAsync();
        Task<byte[]> ExportZimmetToExcelAsync();
        Task<ZımmetliKisiler> ZımmetDurumDegisikligiAsync(int id, int managerid, ZimmetDurum durum);
    }
}
