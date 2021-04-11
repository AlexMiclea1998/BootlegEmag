using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using BootlegEmagService.Product.Models;
using BootlegEmagService.Product;

namespace BootlegEmagService.Controllers
{

    [ApiController]
    
    public class ProductController : ControllerBase
    {

        private string cs = @"URI=file:SQLite\product.db";

        public ProductController()
        {
            productFacede = new ProductFacade(new BootlegEmagService.Product.Repository.ProductRepository());
        }

        private ProductFacade productFacede;

        //[HttpGet]
        //[Route("api/product/createTable")]
        //public IActionResult createTable(Product.Models.Product product)
        //{
        //    //establish connection
        //    using var con = new SQLiteConnection(cs);
        //    con.Open();

        //    //cmd(Query processor)
        //    using var cmd = new SQLiteCommand(con);



        //    //create table again
        //    cmd.CommandText = @"CREATE TABLE product(id INTEGER PRIMARY KEY,
        //    name TEXT, category TEXT, price TEXT, image TEXT)";
        //    cmd.ExecuteNonQuery();

        //    return "Table is created!";

        //}


        [HttpPost]
        [Route("api/product/create")]
        public IActionResult createProduct(BootlegEmagService.Product.Models.createProductDTO Product)
        {
         
         

            //get parameters from Post request
            var name = Product.Name;
            var category = Product.Category;
            var price = Product.Price;
            var image = Product.Image;
            BootlegEmagService.Product.Models.Product product = productFacede.createProduct(name, category, price, image);

            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();

        }
           
        

        [HttpPost]
        [Route("api/product/delete")]
        public IActionResult deleteProduct(BootlegEmagService.Product.Models.deleteProductDTO Product)
        {



            //get parameters from Post request
            var id = Product.Id;
            var name = Product.Name;
            var category = Product.Category;
            var price = Product.Price;
            var image = Product.Image;
            BootlegEmagService.Product.Models.deleteProductDTO product = productFacede.deleteProduct(id, name, category, price, image);

            if (product != null)
            {
                return Ok();
            }
            return BadRequest();

        }
        [HttpPost]
        [Route("api/product/update")]
        public IActionResult updateProduct(Product.Models.updateProductDTO Product)
        {


            //get parameters from Post request
            var id = Product.Id; ;
            var name = Product.Name;
            var category = Product.Category;
            var price = Product.Price;
            var image = Product.Image;


            BootlegEmagService.Product.Models.updateProductDTO product = productFacede.updateProduct(id, name, category, price, image);

            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("api/product/getallProd")]
        public IActionResult getAllProd()
        {

            List<BootlegEmagService.Product.Models.getProductDTO> resp = new List<BootlegEmagService.Product.Models.getProductDTO>();
            
               resp = productFacede.getAllProd();

            if (resp != null)
            {
                return Ok(productFacede.getAllProd());
            }
            //return BadRequest();
            return Ok(productFacede.getAllProd());
        }
    }
}