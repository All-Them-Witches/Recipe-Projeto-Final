namespace Domain.Entities
{
    public class UserRoles
    {
        public int Id { get; set; }
        public User user { get; set; }
        public string Role { get; set; }
        public UserRoles(string role)
        {
            this.Role= role;
        }
    }
}
