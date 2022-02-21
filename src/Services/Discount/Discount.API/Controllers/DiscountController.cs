using Discount.API.Repositories;
using Discount.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepository;
        public DiscountController(IDiscountRepository _discountRepository)
        {
            discountRepository = _discountRepository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var discount = await discountRepository.GetCoupon(productName);
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDiscout(Coupon coupon)
        {
            await discountRepository.CreateCoupon(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscout(Coupon coupon)
        {
            return Ok(await discountRepository.CreateCoupon(coupon));
        }

        [HttpDelete("{productName}", Name = "DeleteDiscout")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscout(string productName)
        {
            return Ok(await discountRepository.DeleteCoupon(productName));
        }

    }
}
