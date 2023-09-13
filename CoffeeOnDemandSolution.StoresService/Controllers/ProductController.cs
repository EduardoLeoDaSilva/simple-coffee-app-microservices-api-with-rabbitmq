using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.StoresService.Data;
using CoffeeOnDemandSolution.StoresService.DTOs;
using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOnDemandSolution.ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private BaseRepository<ApplicationContext, Product> _baseRepository;
        public ProductController(BaseRepository<ApplicationContext, Product> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _baseRepository.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _baseRepository.Query();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO product)
        {
            var productToSave = new Product { CreatedAt = DateTime.Now, Id = Guid.NewGuid(), Name = product.Name, Description = product.Description, Price = product.Price, Stock = product.Stock };

            var result = await _baseRepository.Create(productToSave);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            var productDb = await _baseRepository.GetById(product.Id);
            productDb.Name = product.Name;
            productDb.Description = product.Description;
            productDb.Price = product.Price;
            productDb.Stock = product.Stock;
            productDb.UpdateAt= DateTime.Now;
            var result = await _baseRepository.Update(productDb);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _baseRepository.Delete(id);
            return Ok(result);
        }
    }
}
