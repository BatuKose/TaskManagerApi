using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ZimmetDemirbasRepository : IZimmetDemirbasRepository
    {
        protected RepositoryContext _context;
        public ZimmetDemirbasRepository(RepositoryContext context)
        {
            _context = context;
        }

        public  async Task<Category> InsertCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<Category> SelectCategoryById(int id)
        {
            var category= await _context.Categories.SingleOrDefaultAsync(x=>x.Id==id && x.isActive==true);
            return category;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
