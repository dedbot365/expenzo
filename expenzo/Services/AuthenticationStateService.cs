using expenzo.Models;

namespace expenzo.Services
{
    public class AuthenticationStateService
    {
        private User authenticatedUser;

        public User GetAuthenticatedUser()
        {
            return authenticatedUser; // Retrieve the currently authenticated user
        }

        public void SetAuthenticatedUser(User user)
        {
            authenticatedUser = user; // Set the authenticated user
        }

        public bool IsAuthenticated()
        {
            return authenticatedUser != null; // Check if a user is authenticated
        }

        public void Logout()
        {
            authenticatedUser = null; // Log out the user by setting authenticatedUser to null
        }
    }
}