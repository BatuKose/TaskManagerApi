using ClosedXML.Excel;
using Entites.Data_Transfer_object.ZimmetDemirbas;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Microsoft.EntityFrameworkCore.Query;
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
         public async Task<zimmetKisilerInsertDto>InsertZimmetKisilerAsync(zimmetKisilerInsertDto dto)
        {
            
            if (dto.Unit<=0) throw new BadRequestException("Geçerli miktar bilgisi giriniz");
            var userExits = await _repositoryManager.UserRepository.UserExistsAsync(dto.UserId);
            if (!userExits) throw new NotFoundException("kullanıcı bilgileri bulunamadı");
            var productExist = await _repositoryManager.zimmetDemirbasRepository.SelectProductById(dto.ProcudtId);
            if (productExist is null) throw new NotFoundException("ürün bilgisine ulaşılamadı");
            if (productExist.unit<=0) throw new BadRequestException("zimmete ekleyecek ürünün miktarı yeterli değildir.");
            int mevcutMiktar = productExist.unit;
            if (dto.Unit>mevcutMiktar) throw new BadRequestException("Zimmete alınacak miktar depoda miktarını aşmaktadır");
            int productKalanMiktar = (mevcutMiktar-dto.Unit);
            var zimmet = new ZımmetliKisiler()
            {
                UserId = dto.UserId,
                ProcudtId = dto.ProcudtId,
                Unit = dto.Unit

            };
            productExist.unit = productKalanMiktar;
            _repositoryManager.zimmetDemirbasRepository.InsertZımmetKisiler(zimmet);
            await  _repositoryManager.saveAsyc();


            return new zimmetKisilerInsertDto
            {
                UserId = zimmet.UserId,
                ProcudtId = zimmet.ProcudtId,
                Unit = zimmet.Unit
            };
        }
        public async Task<List<SelectZimmetDetailListDTO>> SelectZimmetDetailsListAsync()
        {
            var zimmetDetails = await _repositoryManager.zimmetDemirbasRepository.SelectZimmetDetailsListAsync();
            if (zimmetDetails is null) throw new NotFoundException("Zimmet detayları bulunamadı");
            return zimmetDetails;
        }
        public async Task<byte[]>ExportZimmetToExcelAsync()
        {
            var list= await _repositoryManager.zimmetDemirbasRepository.SelectZimmetDetailsListAsync();
            if (list is null) throw new NotFoundException("Zimmet detayları bulunamadı");
            using var workbook = new XLWorkbook();
            var worksheet=workbook.Worksheets.Add("Zimmetler");
            
            worksheet.Cell(1, 1).Value = "Adı Soyadı";
            worksheet.Cell(1, 2).Value="Email";
            worksheet.Cell(1, 3).Value="Rol";
            worksheet.Cell(1,4).Value="Ürün Adı";
            worksheet.Cell(1, 5).Value="Ürün model";
            worksheet.Cell(1, 6).Value="Ürün kategori";
            worksheet.Cell(1, 7).Value="Miktar";
            worksheet.Cell(1, 8).Value="Zimmet Tarihi";

            int row = 2;
            foreach (var item in list)
            {
                worksheet.Cell(row, 1).Value = item.ZimmetKisiAd;
                worksheet.Cell(row, 2).Value = item.ZimmetKisiEmail;
                worksheet.Cell(row, 3).Value = item.KisiRol;
                worksheet.Cell(row, 4).Value = item.UrunAd;
                worksheet.Cell(row, 5).Value = item.Model;
                worksheet.Cell(row, 6).Value = item.UrunKategoriAd;
                worksheet.Cell(row, 7).Value = item.ZimmetMiktar;
                worksheet.Cell(row, 8).Value = item.ZimmetTarih.ToString("dd/MM/yyyy");
            };

            worksheet.Columns().AdjustToContents();
            using var stream= new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();

        }
    }
}
