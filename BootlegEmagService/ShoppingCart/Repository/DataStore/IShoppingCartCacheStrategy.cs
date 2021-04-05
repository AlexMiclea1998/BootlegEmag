using BootlegEmagService.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Repository.DataStore
{
    public interface IShoppingCartCacheStrategy
    {
        ShoppingCartModel FindByUserId(string userId);

        void Upsert(ShoppingCartModel model);

        void AddItem(string userId, string productId, int quantity);

        void RemoveItem(string userId, string productId);

        void RemoveQuantity(string userId, string productId, int quantity);
    }
}
