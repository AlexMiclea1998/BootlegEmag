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
        public IActionResult PostDataToDB(Models.UserRegisterDTO userRegisterDTO) { 

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

           

            //get parameters from Post request
            var name = userRegisterDTO.Username;
            var password = userRegisterDTO.Password;
            var role = userRegisterDTO.Role;

            //check if user exists
            string stm = $"SELECT * FROM user WHERE name='{name}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read()) {

                return BadRequest();
            

            //check if role exists
            }else if(role=="ADMIN" || role == "SHOPPER" || role == "SELLER")
             {
                // insert user into table
                 cmd.CommandText = "INSERT INTO user(name, password, role, count) VALUES(@name, @password, @role, @count)";
                 cmd.Parameters.AddWithValue("@name", name);
                 cmd.Parameters.AddWithValue("@password", password);
                 cmd.Parameters.AddWithValue("@role", role);
                 cmd.Parameters.AddWithValue("@count", "1");
                 cmd.Prepare();
                 cmd.ExecuteNonQuery();

                BootlegEmagService.Models.User user = new Models.User(name, password, role);


                return Ok(user); ;

            }
            else
            {
                string error = $"Rolul {role} nu exista!";

                return NotFound(error);
            }
        }
    }
}
