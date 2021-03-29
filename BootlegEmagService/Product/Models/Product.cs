using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Product.Models
{
    public class Product
    {
       
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }


        //public Product(string id,  string name, string category, string price, string image)
        //{
        //    Id = id;
        //    Name = name;
        //    Category = category;
        //    Price = price;
        //    Image = image;

        //}


        public Product(string name, string category, string price, string image)
        {
            Name = name;
            Category = category;
            Price = price;
            Image = image;
        }
    }
}