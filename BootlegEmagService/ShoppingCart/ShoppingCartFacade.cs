using BootlegEmagService.Controllers.ShoppingCart.Http.Models;
using BootlegEmagService.ShoppingCart.Models;
using BootlegEmagService.ShoppingCart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart
{
    public class ShoppingCartFacade
    {
        private ShoppingCartRepository Repository;

        public ShoppingCartFacade(ShoppingCartRepository repository)
        {
            Repository = repository;
        }

        public ShoppingCartModel FindByUserId(string userId)
        {
            return Repository.FindByUserId(userId);
        }
    }
}
