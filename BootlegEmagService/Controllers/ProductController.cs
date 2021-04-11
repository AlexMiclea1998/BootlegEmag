using Microsoft.AspNetCore.Mvc;
using BootlegEmagService.Product.Models;
using BootlegEmagService.Product;

namespace BootlegEmagService.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductFacade Facade { get; set; }

        public ProductController()
        {
            Facade = new ProductFacade();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult CreateProduct(ProductModel product)
        {
            var name = product.Name;
            var category = product.Category;
            var price = product.Price;
            var image = product.Image;
            ProductModel newProduct = Facade.CreateProduct(name, category, price, image);

            if (newProduct != null)
            {
                return Ok(newProduct);
            }
            return BadRequest();

        }

        [Route("delete")]
        [HttpPost]
        public IActionResult deleteProduct(ProductModel product)
        {
            var deletedProduct = Facade.DeleteProduct(product);

            if (deletedProduct != null)
            {
                return Ok();
            }
            return BadRequest();

        }

        [Route("update")]
        [HttpPost]    
        public IActionResult updateProduct(ProductDTO product)
        {
           var updatedProduct = Facade.UpdateProduct(product);

            if (updatedProduct != null)
            {
                return Ok(updatedProduct);
            }
            return BadRequest();

        }

        [Route("getall")]
        [HttpGet]      
        public IActionResult getAllProd()
        {
            var products = Facade.GetAllProducts();
            return Ok(products);
        }
    }
}