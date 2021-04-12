using BootlegEmagService.Models;
using BootlegEmagService.User.Repository;

namespace BootlegEmagService.User
{
    public class UserFacade
    {
        private UserRepository UserRepository { get; set; }

        public UserFacade(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        
        public UserModel Login(string name, string pass)
        {   
                return UserRepository.Find(name,pass);
        }

        public UserModel Register(string username, string password, string role)
        {
            return UserRepository.Register(username, password, role);
        }

        public UserModel DeleteUser(UserModel user)
        {
            return UserRepository.DeleteUser(user);
        }
    }
}
