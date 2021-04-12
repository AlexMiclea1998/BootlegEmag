using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using BootlegEmagService.Models;
using BootlegEmagService.ShoppingCart;
using Microsoft.Extensions.Options;

namespace BootlegEmagService.User.Repository
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(IOptions<DatabaseConfiguration> configuration)
        {
            _connectionString = configuration.Value.UserConnectionString;
        }

        //Login
        public UserModel Find(string name, string pass)
        {
            string password = EncodePasswordToBase64(pass);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int count;

                connection.Open();

                //check if user + password combo exists
                string stm = $"SELECT count FROM user WHERE name='{name}' AND password='{password}'";
                var check = new SQLiteCommand(stm, connection);
                SQLiteDataReader rdr = check.ExecuteReader();
                if (rdr.Read())
                {
                    count = Convert.ToInt32(rdr["count"]);
                    int nextCount = count + 1;
                    string stm2 = $"UPDATE user SET count='{nextCount.ToString()}' WHERE name='{name}'";
                    var check2 = new SQLiteCommand(stm2, connection);
                    SQLiteDataReader rdr1 = check2.ExecuteReader();

                    string stm1 = $"SELECT role FROM user WHERE name='{name}' AND password='{password}'";
                    var check1 = new SQLiteCommand(stm1, connection);
                    SQLiteDataReader rdr2 = check1.ExecuteReader();
                    rdr2.Read();
                    string role = Convert.ToString(rdr2["role"]);
                    connection.Close();

                    UserModel user = new UserModel { Username = name, Password = password, Role = role, Counter = count };

                    return user;
                }
                return null;
                connection.Close();
            }  
        }

        public UserModel Register(string name, string pass, string role )
        {
            string password = EncodePasswordToBase64(pass);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand(connection);
                //check if user exists
                string stm = $"SELECT * FROM user WHERE name='{name}'";
                var check = new SQLiteCommand(stm, connection);
                SQLiteDataReader rdr = check.ExecuteReader();
                if (rdr.Read())
                {
                    return null;
                    //check if role exists
                }
                else if (role == "ADMIN" || role == "SHOPPER" || role == "SELLER")
                {
                    // insert user into table
                    cmd.CommandText = "INSERT INTO user(name, password, role, count) VALUES(@name, @password, @role, @count)";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@count", "1");
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    UserModel user = new UserModel { Username = name, Password = password, Role = role, Counter = 0 };

                    return user;
                }
                else
                {
                    return null;
                }
            }          
        }

        public UserModel DeleteUser(UserModel user)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand(connection);

                //check if user + id combo exists
                string stm = $"SELECT * FROM user WHERE name='{user.Username}'";
                var check = new SQLiteCommand(stm, connection);
                SQLiteDataReader rdr = check.ExecuteReader();
                if (rdr.Read())
                {
                    var command = new SQLiteCommand(_connectionString);
                    command.CommandText = "DELETE FROM user WHERE name = @username";
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return user;
                }
            }
            return null;
        }

        private static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } 
        private static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);

            return result;
        }

    }
}

