using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootlegEmagService.Product.Models;
using BootlegEmagService.Product.Repository;

namespace BootlegEmagService.Product
{
    public class ProductFacade
    {
        private ProductRepository ProductRepository { get; set; }

        public ProductFacade(ProductRepository repository)
        {
            ProductRepository = repository;
        }

        public ProductModel CreateProduct(string name, string category, string price, string image)
        {
            return ProductRepository.CreateProduct( name,  category,  price,  image);
        }

        public ProductModel DeleteProduct(ProductModel product)
        {
            return ProductRepository.DeleteProduct(product);
        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {
            return ProductRepository.UpdateProduct(product);
        }

        public List<ProductDTO> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }
    }
}
