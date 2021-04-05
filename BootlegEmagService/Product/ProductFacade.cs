using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootlegEmagService.Product.Repository;

namespace BootlegEmagService.Product
{
    public class ProductFacade
    {
        private ProductRepository ProductRepository {get; set;}

        public ProductFacade(ProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public Models.Product createProduct(string name, string category, string price, string image)
        {
            return ProductRepository.createProduct( name,  category,  price,  image);
        }

        public Models.deleteProductDTO deleteProduct(string id, string name, string category, string price, string image)
        {
            return ProductRepository.deleteProduct(id, name, category, price, image);
        }
        public Models.updateProductDTO updateProduct(string id, string name, string category, string price, string image)
        {
            return ProductRepository.updateProduct(id, name, category, price, image);
        }
        public List<Models.Product> getAllProd()
        {
            return ProductRepository.getAll();
        }
    }
}
