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

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
        [HttpPut]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart basket)
        {
            try
            {
                await _basketRepository.UpdateShoppingCart(basket);
                return Ok("Update Succesfull");

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
