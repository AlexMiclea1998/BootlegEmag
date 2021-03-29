using BootlegEmagService.ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Controllers.ShoppingCart.Http.Models
{
    public class FindShoppingCartResponse
    {
        public ShoppingCartModel ShoppingCart { get; set; }
    }
}
