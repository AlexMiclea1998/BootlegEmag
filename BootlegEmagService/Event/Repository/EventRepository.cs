using System;
using System.Data.SQLite;
using BootlegEmagService.Events.Model;
using Microsoft.Extensions.Options;
using BootlegEmagService.ShoppingCart;

namespace BootlegEmagService.Events.Repository
{
    public class EventRepository
    {
        private string _connectionString;

        public EventRepository(IOptions<DatabaseConfiguration> configuration)
        {
            _connectionString = configuration.Value.EventConnectionString;
        }

        public void RegisterUserAction(UserActionEvent userAction)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO UserAction(UserId, Type) VALUES(@userId, @type)";
                command.Parameters.AddWithValue("@userId", userAction.UserId);
                command.Parameters.AddWithValue("@type", userAction.Type);
                command.Prepare();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RegisterProductAction(ProductActionEvent productAction)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO ProductAction(UserId, ProductId, Type) VALUES(@userId, @productId, @type)";
                command.Parameters.AddWithValue("@userId", productAction.UserId);
                command.Parameters.AddWithValue("@userId", productAction.ProductId);
                command.Parameters.AddWithValue("@type", productAction.Type);
                command.Prepare();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int GetLoginActionCount(string userId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(connection);
                command.CommandText = "SELECT COUNT(UserId) AS LoginCount FROM UserAction WHERE UserId = @userId AND Type = 0";
                command.Parameters.AddWithValue("@userId", userId);
                string result = command.ExecuteScalar().ToString();
                return int.Parse(result);
            }     
        }

        public void RegisterDiscountForUser(string userId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO Discounts(UserId, Discount) VALUES(@userId, @discount)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@discount", 20);
                command.Prepare();
                command.ExecuteNonQuery();
                connection.Close();
            }            
        }
    }
}
