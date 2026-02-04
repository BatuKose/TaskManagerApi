using Entites.Data_Transfer_object.ZimmetDemirbas;
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
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
        public async Task<Product>InsertProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Task<Product> SelectProductById(int id)
        {
           var product= _context.Products.SingleOrDefaultAsync(x=>x.id==id && x.isActive==true);
           return product;
        }
        public async Task<List<UrünBilgileriDTO>> GetProductsWithCategoryAsync()
        {
            var query  = await (from p in _context.Products.AsNoTracking()
                        join c in _context.Categories.AsNoTracking() on p.categoryId equals c.Id
                        where p.isActive==true && c.isActive==true
                        select new UrünBilgileriDTO
                        {
                            urunAd=p.name,
                            urunMarka=p.brand,
                            urunModel=p.model,
                            urunKategori=c.Name

                        }).ToListAsync();
            return  query;
        }
       
        public async Task<bool> CategorySilinmeyeMüsaitMi(int catId)
        {
            return await _context.Products.AnyAsync(x=> x.categoryId == catId);
           
        }
        public  ZımmetliKisiler InsertZımmetKisiler(ZımmetliKisiler zımmetliKisiler)
        {
            _context.zımmetliKisiler.Add(zımmetliKisiler);     
            return zımmetliKisiler;
        }
        public async Task<List<SelectZimmetDetailListDTO>> SelectZimmetDetailsListAsync()
        {
            var query = await (
                from z in _context.zımmetliKisiler.AsNoTracking()
                join p in _context.Products.AsNoTracking() on z.ProcudtId equals p.id
                join u in _context.users.AsNoTracking() on z.UserId equals u.Id
                join r in _context.roles.AsNoTracking() on u.RoleId equals r.Id
                join c in _context.Categories.AsNoTracking() on p.categoryId equals c.Id
                select new SelectZimmetDetailListDTO
                {
                    ZimmetKisiAd=u.UserName,
                    ZimmetKisiEmail=u.Email,
                    KisiRol=r.RoleName,
                    UrunAd=p.name,
                    Model=p.model,
                    UrunKategoriAd=c.Name,
                    ZimmetMiktar=z.Unit,
                    ZimmetTarih=z.ZimmetAlisTarihi
                }
                ).ToListAsync();
            return query;
        }
    }
}
