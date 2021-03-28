using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using BootlegEmagService.Models;

namespace BootlegEmagService.Controllers
{
    
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        //relative path to db File
        private string cs = @"URI=file:SQLite\user.db";

        [HttpPost]
        public IActionResult login(UserLoginDTO userLogin)
        {
            int count;
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            var name = userLogin.Username;
            var password = userLogin.Password;

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

                Models.User user = new User("Test", "testpass", "ADMIN");


                return Ok(user);
                


            }

            return BadRequest();

        }
    }
}
