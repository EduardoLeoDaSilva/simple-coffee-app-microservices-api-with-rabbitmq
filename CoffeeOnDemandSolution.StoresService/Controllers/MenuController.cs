using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.StoresService.Data;
using CoffeeOnDemandSolution.StoresService.DTOs;
using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOnDemandSolution.StoresService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private BaseRepository<ApplicationContext, Menu> _baseRepository;
        private BaseRepository<ApplicationContext, Product> _productRepository;
        private BaseRepository<ApplicationContext, Store> _storeRepository;
        public MenuController(BaseRepository<ApplicationContext, Menu> baseRepository, BaseRepository<ApplicationContext, Product> productRepository, BaseRepository<ApplicationContext, Store> storeRepository)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
            _storeRepository = storeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
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
        public async Task<IActionResult> Create([FromBody] MenuDTO menu)
        {
            var menuToSave = new Menu { CreatedAt = DateTime.Now, Id = Guid.NewGuid(), Name = menu.Name, Products = new List<Product>(), Stores = new List<Store>() };

            foreach (var item in menu.ProductIds)
            {
                var product = await _productRepository.GetById(item);
                menuToSave.Products.Add(product);
            }
            foreach (var item in menu.StoreIds)
            {
                var store = await _storeRepository.GetById(item);
                menuToSave.Stores.Add(store);

            }

            var result = await _baseRepository.Create(menuToSave);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MenuDTO menu)
        {
            var menuDb = await _baseRepository.GetById(menu.Id);
            menuDb.Name = menu.Name;
            foreach (var item in menu.ProductIds)
            {
                menuDb.Products.Add(new Product { Id = item });
            }
            foreach (var item in menu.StoreIds)
            {
                menuDb.Stores.Add(new Store { Id = item });

            }
            var result = await _baseRepository.Update(menuDb);
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
