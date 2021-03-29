using BootlegEmagService.Controllers.ShoppingCart.Http.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BootlegEmagService.ShoppingCart
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private ShoppingCartFacade Facade;
        public ShoppingCartController(ShoppingCartFacade facade)
        {
            Facade = facade;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var shoppingCart = Facade.FindByUserId(id);
            
            if (shoppingCart != null)
            {
                var shoppingCartResponse = new FindShoppingCartResponse { ShoppingCart = shoppingCart };
                return Ok(shoppingCartResponse);
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPut]
        public void Post([FromBody] AddItemToCartRequest request)
        {
        }

        [HttpDelete]
        public void Delete([FromBody] RemoveItemFromCartRequest request)
        {

        }
    }
}
