﻿using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        //relative path to db File
        private string cs = @"URI=file:SQLite\user.db";

        [HttpPost]
        [Route("api/user/delete")]
        public String deleteUser(string name, string id)
        {

            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //get parameters from Post request
            name = Request.Form["name"];
            id = Request.Form["id"];

           
            //check if user + id combo exists
            string stm = $"SELECT * FROM user WHERE name='{name}' AND id='{id}'";
            using var check = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = check.ExecuteReader();
            if (rdr.Read())
            {

                //delete user
                string stm2 = $"DELETE FROM user WHERE name='{name}' AND id='{id}'";
                using var check2 = new SQLiteCommand(stm2, con);
                return $"User {name} with Id : {id} was successfully deleted!";
            }

            return $"No user {name} and id: {id} found!";

        }


       [HttpGet]
       [Route("api/user/dropAllUsers")]
       public String dropAllUsers()
        {
            //establish connection
            using var con = new SQLiteConnection(cs);
            con.Open();

            //cmd(Query processor)
            using var cmd = new SQLiteCommand(con);

            //drop entire table
            cmd.CommandText = "DROP TABLE IF EXISTS user";
            cmd.ExecuteNonQuery();

            //create table again
            cmd.CommandText = @"CREATE TABLE user(id INTEGER PRIMARY KEY,
            name TEXT, password TEXT, role TEXT, count TEXT)";
            cmd.ExecuteNonQuery();

            return "Table is clear!";

        }


    }
}
