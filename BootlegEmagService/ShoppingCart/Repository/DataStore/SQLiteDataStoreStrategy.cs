using BootlegEmagService.ShoppingCart.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart.Repository.DataStore
{
    internal class SQLiteDataStoreStrategy : IShoppingCartDataStoreStrategy
    {
        private SQLiteReaderShoppingCartConverter Converter { get; }

        private string _connectionString;
        private static object _lock = new object();

        public SQLiteDataStoreStrategy(IOptions<DatabaseConfiguration> configuration, SQLiteReaderShoppingCartConverter converter)
        {
            _connectionString = configuration.Value.ShoppingCartConnectionString;
            Converter = converter;
        }

        public ShoppingCartModel FindByUserId(string userId)
        {
            lock(_lock)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(connection);
                    command.CommandText = "SELECT * FROM ShoppingCart WHERE userId = @UserId";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Prepare();
                    var reader = command.ExecuteReader();

                    return Converter.Convert(reader);
                }
            }
        }

        public void AddItem(string userId, string productId, int quantity)
        {
            lock(_lock)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    var command = new SQLiteCommand(connection);
                    command.CommandText = "INSERT INTO ShoppingCart(userId, productId, quantity) VALUES('@UserId', '@ProductId', @QuantityToBeAdded)"
                        + "ON CONFLICT(userId, productId) DO UPDATE SET quantity=quantity+@quantityToBeAdded";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@QuantityToBeAdded", quantity);
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveQuantity(string userId, string productId, int quantity)
        {

        }

        public void RemoveItem(string userId, string productId)
        {

        }
    }
}
