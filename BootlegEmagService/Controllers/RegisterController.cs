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
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {

        private string cs = @"URI=file:SQLite\user.db";


        [HttpPost]
        public String PostDataToDB(string name, string password, string role) { 

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            name = Request.Form["name"];
            password = Request.Form["password"];
            role = Request.Form["role"];

            //check if user exists
            string stm = $"SELECT * FROM user WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read()) {
                return "user deja existent";
            
            }else
             {
                // insert user into table
                 cmd.CommandText = "INSERT INTO user(name, password, role ) VALUES(@name, @password, @role)";
                 cmd.Parameters.AddWithValue("@name", name);
                 cmd.Parameters.AddWithValue("@password", password);
                 cmd.Parameters.AddWithValue("@role", role);
                 cmd.Prepare();
                 cmd.ExecuteNonQuery();

                 return "User adaugat";

             }
            return "nothing";
        }
    }
}
