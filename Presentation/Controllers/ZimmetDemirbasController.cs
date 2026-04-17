using Entites.Data_Transfer_object.ZimmetDemirbas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.ZimmetDurumEnums;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("ZimmetDemirbas")]
//    [Authorize]
    public class ZimmetDemirbasController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ZimmetDemirbasController(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }

        [HttpPost("InsertCategory")]
        public async Task<IActionResult> InsertCategoryAsync([FromBody] CreateCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.CreateCategoryAsync(categoryDTO);
            return Ok("Kategori başarıyla eklendi.");
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryDTO updateCategoryDTO, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.UpdateCategoryAsync(updateCategoryDTO, id);
            return Ok("Kategori başarıyla güncellendi.");
        }
        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteCategoryAsync(int id)
        {
            await _serviceManager.ZimmetDemirbasService.SoftDeleteCategoryAsync(id);
            return Ok("Kategori başarıyla silindi.");
        }
        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProductAsync([FromBody] CreateProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.InsertProductAsync(product);
            return Ok("Ürün başarıyla eklendi.");
        }
        [HttpPut("soft-delete-product/{id}")]
        public async Task<IActionResult> SoftDeleteProductAsync(int id)
        {
            await _serviceManager.ZimmetDemirbasService.SoftDeleteProductAsync(id);
            return Ok();
        }
        [HttpGet("ürün-listesi")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _serviceManager.ZimmetDemirbasService.GetProductsWithCategoryAsync();
            return Ok(products);
        }

        [HttpPost("ZimmetKisiler")]
        public async Task<IActionResult> zimmetKisilerInsertAsync([FromBody] zimmetKisilerInsertDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.InsertZimmetKisilerAsync(dto);
            return Ok();
        }
        [HttpGet("ZimmetDetayListesi")]
        public async Task<IActionResult> GetZimmetDetailsListAsync()
        {
            var zimmetDetails = await _serviceManager.ZimmetDemirbasService.SelectZimmetDetailsListAsync();
            return Ok(zimmetDetails);
        }
        [HttpGet("export-excel-zimmetkisiler")]
        public async Task<IActionResult> ExportZimmetKisilerToExcelAsync()
        {
            var file = await _serviceManager.ZimmetDemirbasService.ExportZimmetToExcelAsync();
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Zimmetler_{DateTime.Now:yyyyMMdd}.xlsx"
            );
        }
        [HttpPatch("zimmetDurumDegisikligi")]
        public async Task<IActionResult> ZimmetDurumDegistirAsync(int id, int managerid, ZimmetDurum durum)
        {
            await _serviceManager.ZimmetDemirbasService.ZımmetDurumDegisikligiAsync(id, managerid, durum);
            return Ok("Zimmet durumu başarıyla değiştirildi.");
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult>AktifZimmetleriKategorileriGetir()
        {
            var result= await _serviceManager.ZimmetDemirbasService.GetGategorty();
            return Ok(result);
        }
        [HttpGet("getuserforzimmet")]
        public async Task<IActionResult>getUserForZimmet()
        {
            var user = await _serviceManager.ZimmetDemirbasService.GetUserForZimmet();
            return Ok(user);
        }
        [HttpPatch("zimmetiade")]
        public async Task<IActionResult> ZimmetIade([FromQuery] int dosyaid, [FromQuery] int miktar)
        {
            var result = await _serviceManager.ZimmetDemirbasService.ZimmetIadeAsync(dosyaid, miktar);
            return NoContent();
        }
    }
}
