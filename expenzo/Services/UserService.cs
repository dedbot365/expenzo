using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Concurrent;

namespace expenzo.Services
{
    public class UserService : IUserService
    {
        private readonly UserDao _userDao;
        private static ConcurrentDictionary<string, bool> _loggedInUsers = new ConcurrentDictionary<string, bool>();

        public UserService(UserDao userDao)
        {
            _userDao = userDao;
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await UserExists(user.UserName))
            {
                return false;
            }

            _userDao.AddUser(user);
            return true;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            var user = await _userDao.GetUser(username, password);
            if (user != null)
            {
                _loggedInUsers[username] = true;
            }
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userDao.UserExists(username);
        }

        public Task<bool> IsUserLoggedIn()
        {
            return Task.FromResult(_loggedInUsers.ContainsKey("username") && _loggedInUsers["username"]);
        }
    }
}