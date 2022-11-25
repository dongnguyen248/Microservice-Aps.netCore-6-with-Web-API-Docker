using Basket.API.DiscountGrpcServices;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {

            var basket = await _basketRepository.GetShoppingCart(userName);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                foreach (var item in basket.ShoppingCartitems)
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
            };

                return Ok(await _basketRepository.UpdateShoppingCart(basket));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteShoppingCart(userName);
                return Ok("Delete Succesfull");

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
