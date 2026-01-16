using Entites.Data_Transfer_object.ZimmetDemirbas;
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

    }
}
