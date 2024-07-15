using System.Security.Principal;

namespace Application.Contracts.UserContracts
{
    public class LoginUserCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
