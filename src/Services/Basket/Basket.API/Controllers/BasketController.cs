using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.AspNetCore.Http.Connections;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly DiscountGrpcService discountGrpcService;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public BasketController(IBasketRepository _basketRepository,
            DiscountGrpcService _discountGrpcService, IMapper _mapper,
            IPublishEndpoint _publishEndpoint)
        {
            basketRepository = _basketRepository ?? throw new ArgumentNullException(nameof(BasketController));
            discountGrpcService = _discountGrpcService ?? throw new ArgumentNullException(nameof(BasketController));
            mapper = _mapper ?? throw new ArgumentNullException(nameof(BasketController));
            publishEndpoint = _publishEndpoint ?? throw new ArgumentNullException(nameof(BasketController));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await basketRepository.GetBasketAsync(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart model)
        {
            foreach (var item in model.Items)
            {
                var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await basketRepository.UpdateBasketAsync(model));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await basketRepository.DeleteBasketAsync(userName);
            return Ok(new {Result = "Deleted"});
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout model)
        {
            var baskets = await basketRepository.GetBasketAsync(model.UserName);
            if (baskets == null)
                return BadRequest();

            var eventMessage = mapper.Map<BasketCheckoutEvent>(model);

            await basketRepository.DeleteBasketAsync(baskets.UserName);
            eventMessage.TotalPrice = baskets.TotalPrice;
            await publishEndpoint.Publish(eventMessage);

            return Accepted();
        }
    }
}