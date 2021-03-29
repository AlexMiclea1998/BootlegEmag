using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BootlegEmagService.Controllers
{

    [ApiController]
    
    public class ProductController : ControllerBase
    {

        private string cs = @"URI=file:SQLite\product.db";

        [HttpGet]
        [Route("api/product/createTable")]
        public String createTable()
        {
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

          

            //create table again
            cmd.CommandText = @"CREATE TABLE product(id INTEGER PRIMARY KEY,
            name TEXT, category TEXT, price TEXT, image TEXT)";
            cmd.ExecuteNonQuery();

            return "Table is created!";

        }


        [HttpPost]
        [Route("api/product/create")]
        public String createProduct(string name, string category, string price, string image)
        {
         
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();
          

            //cmd(Query processor)dotnet add package Microsoft.Data.Sqlite
            using var cmd = new SQLiteCommand(con);

         

            //get parameters from Post request
            name = Request.Form["name"];
            category = Request.Form["category"];
            price = Request.Form["price"];
            image = Request.Form["image"];


            //check if user exists
            string stm = $"SELECT * FROM product WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                return "produsul deja exista";


                //check if role exists
            }else
            {
                // insert user into table
                cmd.CommandText = "INSERT INTO product(name, category, price, image) VALUES(@name, @category, @price, @image)";
                //cmd.Parameters.AddWithValue("@id");
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@image", image);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                return $"Produs {name} adaugat !";

            }
           
        }

        [HttpPost]
        [Route("api/product/delete")]
        public String deleteProduct(string name)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            name = Request.Form["name"];
           // id = Request.Form["id"];


            //check if user + id combo exists
            string stm = $"SELECT * FROM product WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                //delete product
                string stm2 = $"DELETE FROM product WHERE name='{name}'";
                using var check2 = new SQLiteCommand(stm2, con);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();
                return $"Product {name}  was successfully deleted!";
            }

            return $"No product {name} found!";

        }
        [HttpPost]
        [Route("api/product/update")]
        public String updateProduct(string id, string name, string category, string price, string image)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            id = Request.Form["id"];
            name = Request.Form["name"];
            category = Request.Form["category"];
            price = Request.Form["price"];
            image = Request.Form["image"];


            
            string stm = $"SELECT * FROM product WHERE id='{id}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                string stm2 = $"UPDATE product SET name= '{name}', category='{category}', price='{price}', image='{image}' WHERE id='{id}'";
                using var check2 = new SQLiteCommand(stm2, con);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();

                return $"Produs {name} a fost modificat cu succes !";


                
            }
            else
            {

                return "Produsul nu exista!";
            }

        }
    }
}