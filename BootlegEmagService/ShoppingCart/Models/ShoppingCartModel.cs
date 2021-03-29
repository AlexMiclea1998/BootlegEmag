using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Models
{
    public class ShoppingCartModel
    {
        public string UserId { get; set; }

        public IDictionary<string, int> ProductIdQuantity { get; set; }
    }
}
