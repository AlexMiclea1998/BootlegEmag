using BootlegEmagService.ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Repository.DataStore
{
    public class SQLiteReaderShoppingCartConverter
    {
        public ShoppingCartModel Convert(SQLiteDataReader reader)
        {
            return new ShoppingCartModel { UserId = "123", ProductIdQuantity = new Dictionary<string, int>() };
        }
    }
}
