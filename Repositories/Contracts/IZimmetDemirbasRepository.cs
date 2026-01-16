using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IZimmetDemirbasRepository
    {
        Task<Category>InsertCategoryAsync(Category category);
        Task<Category>SelectCategoryById(int id);
       Task SaveAsync();
    }
}
