using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;

namespace BootlegEmagService.Controllers
{
   
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        string cs = @"URI=file:SQLite\user.db";
       

        [HttpGet]   // GET /api/test2
        public String test()
        {
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            
            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);
            

            cmd.CommandText = "INSERT INTO cars(name, price) VALUES(@name, @price)";
            cmd.Parameters.AddWithValue("@name", "Audi");
            cmd.Parameters.AddWithValue("@price", 36600);
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            
            String test = "OK";

            return test;
        }
    }
}
