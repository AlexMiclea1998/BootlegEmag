using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BootlegEmagService.User.Repository
{
    public class UserRepository
    {
       

        //relative path to db File
        private string cs = @"URI=file:SQLite\user.db";

        //Login
        public Models.User find(string name, string password)
        {

            int count;
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);



            //check if user + password combo exists
            string stm = $"SELECT count FROM user WHERE name='{name}' AND password='{password}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {
                count = Convert.ToInt32(rdr["count"]);
                int nextCount = count + 1;
                string stm2 = $"UPDATE user SET count='{nextCount.ToString()}' WHERE name='{name}'";
                using var check2 = new SQLiteCommand(stm2, con);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();

                string stm1 = $"SELECT role FROM user WHERE name='{name}' AND password='{password}'";
                using var check1 = new SQLiteCommand(stm1, con);
                using SQLiteDataReader rdr2 = check1.ExecuteReader();
                rdr2.Read();
                string role = Convert.ToString(rdr2["role"]);

                Models.User user = new Models.User(name, password, role, count);

               
                return user;
            }
            return null;
        }

        //Register
        public Models.User register(string name, string password, string role )
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);


            //check if user exists
            string stm = $"SELECT * FROM user WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
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

                BootlegEmagService.Models.User user = new Models.User(name, password, role,0);

                return user;

            }
            else
            {
                return null;
            }
        }
    }
}

