using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.OrdersService.Data;
using CoffeeOnDemandSolution.OrdersService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOnDemandSolution.OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private BaseRepository<ApplicationContext, Order> _baseRepository;
        private readonly Publisher _publisher;
        public OrderController(BaseRepository<ApplicationContext, Order> baseRepository, Publisher publisher)
        {
            _baseRepository = baseRepository;
            _publisher = publisher;
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Order order)
        {
            order.Status = PaymentStatus.Processing;
            order.DeletedAt = null;
            order.UpdateAt = null;
            order.Id = Guid.NewGuid();
            var result = await _baseRepository.Create(order);
            if (result)
                _publisher.Publish<dynamic>(new  { OrderId = order.Id, PaymentType = "CreditCard", ProductId = order.ProductId}, "OrderPayment");

            return Ok(result);
        }
    }
}
