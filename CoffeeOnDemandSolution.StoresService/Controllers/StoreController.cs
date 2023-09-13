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
    public class StoreController : ControllerBase
    {
        private BaseRepository<ApplicationContext, Store> _baseRepository;
        public StoreController(BaseRepository<ApplicationContext, Store> baseRepository)
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
        public async Task<IActionResult> Create([FromBody] StoreDTO store)
        {
            var storeToSave = new Store { CreatedAt = DateTime.Now, Id = Guid.NewGuid(), Name = store.Name, Address = new Address { City = store.City, State = store.State, FullAddress = store.FullAddress } };

            var result = await _baseRepository.Create(storeToSave);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StoreDTO store)
        {

            var storeDb = await _baseRepository.GetById(store.Id);
            storeDb.Name = store.Name;
            storeDb.UpdateAt = DateTime.Now;
            storeDb.Id = storeDb.Id;

            foreach (var item in store.MenuIds)
            {
                storeDb.Menus.Add(new Menu { Id = item });
            }
            storeDb.Address.City = store.City;
            storeDb.Address.State = store.State;
            storeDb.Address.FullAddress = store.FullAddress;

            var result = await _baseRepository.Update(storeDb);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var result = await _baseRepository.Delete(id);
            return Ok(result);
        }
    }
}
