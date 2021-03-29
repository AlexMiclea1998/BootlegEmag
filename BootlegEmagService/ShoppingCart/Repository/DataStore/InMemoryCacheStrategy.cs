using BootlegEmagService.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Repository.DataStore
{
    internal class InMemoryCacheStrategy : IShoppingCartCacheStrategy
    {
        public void AddItem(string userId, string productId, int quantity)
        {
        }

        public ShoppingCartModel FindByUserId(string userId)
        {
            return null;
        }

        public void RemoveItem(string userId, string productId)
        {
        }

        public void RemoveQuantity(string userId, string productId, int quantity)
        {
        }

        public void Upsert(ShoppingCartModel model)
        {
        }
    }
}
