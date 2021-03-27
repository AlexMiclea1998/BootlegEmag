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
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        //relative path to db File
        private string cs = @"URI=file:SQLite\user.db";

        [HttpPost]
        public String login(string name, string password)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            name = Request.Form["name"];
            password = Request.Form["password"];

            //check if user + password combo exists
            string stm = $"SELECT * FROM user WHERE name='{name}' AND password='{password}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {
                return "successful";
            }

                return "fail";

        }



    }
}
