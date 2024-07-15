using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Contracts.UserContracts
{
    public class UserViewModel : CreateUserCommand
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public List<UserRoles> Roles { get; set; }
    }
}
