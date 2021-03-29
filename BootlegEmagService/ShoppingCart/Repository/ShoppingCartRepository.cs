using BootlegEmagService.Exceptions;
using BootlegEmagService.ShoppingCart.Models;
using BootlegEmagService.ShoppingCart.Repository.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Repository
{
    public class ShoppingCartRepository
    {
        private IShoppingCartCacheStrategy Cache { get; }

        private IShoppingCartDataStoreStrategy DataStore { get; }

        public ShoppingCartRepository(IShoppingCartCacheStrategy cache, IShoppingCartDataStoreStrategy dataStore)
        {
            Cache = cache;
            DataStore = dataStore;
        }

        public ShoppingCartModel FindByUserId(string userId)
        {
            var model = Cache.FindByUserId(userId);

            if (model == null)
            {
                model = DataStore.FindByUserId(userId);
                Cache.Upsert(model);
            }

            if (model == null)
            {
                throw new ServiceInternalErrorException { ErrorMessage = "The shopping cart could not be found." };
            }

            return model;
        }

        public void AddItem(string userId, string productId, int quantity)
        {
            DataStore.AddItem(userId, productId, quantity);
            Cache.AddItem(userId, productId, quantity);
        }

        public void RemoveItem(string userId, string productId, int quantity)
        {
            var model = FindByUserId(userId);
            
            if (!model.ProductIdQuantity.ContainsKey(productId))
            {
                throw new ServiceInternalErrorException { ErrorMessage = "The product could not be found" };
            }

            int quantityLeft = model.ProductIdQuantity[productId] - quantity;
            if (quantityLeft <= 0)
            {
                DataStore.RemoveItem(userId, productId);
                Cache.RemoveItem(userId, productId);
            }
            else
            {
                DataStore.RemoveQuantity(userId, productId, quantity);
                Cache.RemoveQuantity(userId, productId, quantity);
            }
        }
    }
}
