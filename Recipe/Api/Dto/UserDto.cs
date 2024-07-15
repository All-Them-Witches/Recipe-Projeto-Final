using Application.Contracts.UserContracts;

namespace Recipe.Api.Dto
{
    public class UserDto : UserViewModel
    {
        public List<Links> links { get; set; }
    }
}
