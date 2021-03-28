using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Models
{
    public class Product
    {

        public string name { get; set; }
        public string category { get; set; }
        public string  price { get; set; }
        public string image { get; set; }


        public Product(string name, string category, string price, string image)
        {
          
            this.name = name;
            this.category = category;
            this.price = price;
            this.image = image;

        }
    }
}
