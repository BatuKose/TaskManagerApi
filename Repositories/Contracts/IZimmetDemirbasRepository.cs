using Entites.Data_Transfer_object.ZimmetDemirbas;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IZimmetDemirbasRepository
    {
        Task SaveAsync();
        Task<Category>InsertCategoryAsync(Category category);
        Task<Category>SelectCategoryById(int id);
        Task<Product> InsertProductAsync(Product product);
        Task<Product> SelectProductById(int id);
        Task<List<UrünBilgileriDTO>> GetProductsWithCategoryAsync();
        Task<bool> CategorySilinmeyeMüsaitMi(int catId);
        ZımmetliKisiler InsertZımmetKisiler(ZımmetliKisiler zımmetliKisiler);
        Task<List<SelectZimmetDetailListDTO>> SelectZimmetDetailsListAsync();
        Task<ZımmetliKisiler> GetByIdAsync(int id);
        Task<List<Category>> GetCategorysync();


    }
}
