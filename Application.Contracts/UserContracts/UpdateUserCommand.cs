namespace Application.Contracts.UserContracts
{
    public class UpdateUserCommand : CreateUserCommand
    {
        public int Id { get; set; }
    }
}
