using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetDiscount(string productName)
        {
            //var discount = await _discountRepository.GetDiscount(productName);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> CreateDiscount(Coupon coupon)
        {
            //await _discountRepository.CreateDiscount(coupon);
            return Ok();    
        }
        [HttpPut]
        public async Task<ActionResult> UpdateDiscount(Coupon coupon)
        {
            //await _discountRepository.UpdateDiscount(coupon);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            //await _discountRepository.DeleteDiscount(productName);
            return Ok();
        }

        
    }
}
