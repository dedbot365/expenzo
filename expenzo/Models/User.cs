using System.ComponentModel.DataAnnotations;

namespace expenzo.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; }
    }
}