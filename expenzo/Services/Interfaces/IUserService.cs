using expenzo.Models;
namespace expenzo.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<User> LoginUser(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> IsUserLoggedIn();
        Task<bool> UpdateUser(User user); 
    }
}