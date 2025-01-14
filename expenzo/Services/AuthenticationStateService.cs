using expenzo.Models;

namespace expenzo.Services
{
    public class AuthenticationStateService
    {
        private User authenticatedUser;

        public User GetAuthenticatedUser()
        {
            return authenticatedUser;
        }

        public void SetAuthenticatedUser(User user)
        {
            authenticatedUser = user;
        }

        public bool IsAuthenticated()
        {
            return authenticatedUser != null;
        }

        public void Logout()
        {
            authenticatedUser = null;
        }
    }
}