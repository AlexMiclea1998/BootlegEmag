using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BootlegEmagService.Product.Repository
{
    public class ProductRepository
    {


        //relative path to db File
        private string cs = @"URI=file:SQLite\product.db";



        //Register
        public Models.Product createProduct(string name, string category, string price, string image)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);


            //check if user exists
            string stm = $"SELECT * FROM product WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                return null;



            }
            else
            {
                // insert product into table
                cmd.CommandText = "INSERT INTO product(name, category, price, image) VALUES(@name, @category, @price, @image)";

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@image", image);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                BootlegEmagService.Product.Models.Product product = new Models.Product(name, category, price, image);


                return product;

            }
        }

        public Models.deleteProductDTO deleteProduct(string id, string name, string category, string price, string image)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request




            //check if user + id combo exists
            string stm = $"SELECT * FROM product WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                //delete product
                string stm2 = $"DELETE FROM product WHERE name='{id}'";
                using var check2 = new SQLiteCommand(stm2, con);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();
                BootlegEmagService.Product.Models.deleteProductDTO product = new BootlegEmagService.Models.deleteProductDTO(id, name, category, price, image);
                return product;
            }

            return null;

        }
        public Models.updateProductDTO updateProduct(string id, string name, string category, string price, string image)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

         



            string stm = $"SELECT * FROM product WHERE id='{id}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                string stm2 = $"UPDATE product SET name= '{name}', category='{category}', price='{price}', image='{image}' WHERE id='{id}'";
                using var check2 = new SQLiteCommand(stm2, con);
                using SQLiteDataReader rdr1 = check2.ExecuteReader();

                BootlegEmagService.Product.Models.updateProductDTO product = new BootlegEmagService.Models.updateProductDTO(id, name, category, price, image);

                return product;



            }
            else
            {

                return null;
            }

        }

        public List<Models.Product> getAll()
        {
            List<Models.Product> list = new List<Models.Product>();
            int i = 0;
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);


            string stm = $"SELECT * FROM product";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
           
                while (rdr.Read()) { 
                
                string id = Convert.ToString(rdr["id"]);
                string name = Convert.ToString(rdr["name"]);
                string category = Convert.ToString(rdr["category"]);
                string price = Convert.ToString(rdr["price"]);
                string image = Convert.ToString(rdr["image"]);
                list.Add(new Models.Product(name, category, price, image));
               
            }
            return list;



        }
    }
}