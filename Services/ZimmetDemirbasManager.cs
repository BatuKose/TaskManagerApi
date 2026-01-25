using Entites.Data_Transfer_object.ZimmetDemirbas;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ZimmetDemirbasManager : IZimmetDemirbasService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ZimmetDemirbasManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public  async Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var categoryRepo = new Category()
            {
                Name = createCategoryDTO.Name,
                Description = createCategoryDTO.Description,
                
            };
            await _repositoryManager.zimmetDemirbasRepository.InsertCategoryAsync(categoryRepo);
            return new CreateCategoryDTO 
            {
                Name = categoryRepo.Name,
                Description = categoryRepo.Description,
            };
        }

        public async Task<UpdateCategoryDTO> UpdateCategoryAsync(UpdateCategoryDTO dto, int id)
        {
            var category = await _repositoryManager
                .zimmetDemirbasRepository.SelectCategoryById(id);

            if (category is null) throw new Exception("Kategori bulunamadı");

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _repositoryManager.zimmetDemirbasRepository.SaveAsync();

            return new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
        public async Task<Category> SoftDeleteCategoryAsync(int id)
        {
            var Categorty = await _repositoryManager.zimmetDemirbasRepository.SelectCategoryById(id);
            bool categoryKullaniliyorMu;
            if ( categoryKullaniliyorMu = await _repositoryManager.zimmetDemirbasRepository.CategorySilinmeyeMüsaitMi(Categorty.Id))
            {
                throw new BadRequestException("Silinmek istenen kategori bir ürün tarafından kullanılmaktadır silinemez");
            }
            else
            {
                if (Categorty is null) throw new Exception("Kategori bulunamadı");
                Categorty.isActive = false;
                await _repositoryManager.zimmetDemirbasRepository.SaveAsync();
                return Categorty;
            }
          
        }
        public async Task<CreateProductDto>InsertProductAsync(CreateProductDto dto)
        {
            var product = new Product()
            {
                categoryId = dto.categoryId,
                name = dto.name,
                brand = dto.brand,
                model = dto.model,
                description = dto.description,
                unit = dto.unit

            };
            var categoryİsExist= await _repositoryManager.zimmetDemirbasRepository.SelectCategoryById(product.categoryId);
            if (categoryİsExist is null) throw new NotFoundException("Kategori bulunamadı");

            await _repositoryManager.zimmetDemirbasRepository.InsertProductAsync(product);
          
            return new CreateProductDto { 
                categoryId= product.categoryId,
                name=product.name,
                brand=product.brand,
                model=product.model,
                description=product.description,
                unit=product.unit
            };
        }
        public async Task<Product> SoftDeleteProductAsync(int id)
        {
           var prodcut= await _repositoryManager.zimmetDemirbasRepository.SelectProductById(id);
            if (prodcut is null) throw new NotFoundException("Ürün bulunamadı");
            prodcut.isActive = false;
            await _repositoryManager.zimmetDemirbasRepository.SaveAsync();
            return prodcut;
        }

        public Task<List<UrünBilgileriDTO>> GetProductsWithCategoryAsync()
        {
            var productsWithCategory = _repositoryManager.zimmetDemirbasRepository.GetProductsWithCategoryAsync();
            if(productsWithCategory is null) throw new NotFoundException("Ürünler bulunamadı");
            return productsWithCategory;
        }
      
    }
}
