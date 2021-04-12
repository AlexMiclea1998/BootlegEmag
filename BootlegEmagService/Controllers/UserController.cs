using Microsoft.AspNetCore.Mvc;
using BootlegEmagService.User;
using BootlegEmagService.Models;

namespace BootlegEmagService.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private UserFacade Facade;
        public UserController(UserFacade facade)
        {
            Facade = facade;
        }

        //Login Done
        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLoginDTO userLogin)
        {
            //get parameters from Post request
            string name = userLogin.Username.ToString();
            string password = userLogin.Password.ToString();

            UserModel user = Facade.Login(name, password);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        //Register done
        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegisterDTO userRegisterDTO)
        {
            //get parameters from Post request
            var name = userRegisterDTO.Username;
            var password = userRegisterDTO.Password;
            var role = userRegisterDTO.Role;
            UserModel user = Facade.Register(name, password, role);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteUser(UserModel user)
        {
            var deletedUser = Facade.DeleteUser(user);

            if (deletedUser != null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}


