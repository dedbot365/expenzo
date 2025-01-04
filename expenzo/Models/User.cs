using expenzo.Base;

namespace expenzo.Models;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    //public Currency Currency { get; set; }
    public string Currency { get; set; }
}