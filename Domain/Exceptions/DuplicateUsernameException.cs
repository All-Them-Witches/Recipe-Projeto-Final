namespace Domain.Exceptions
{
    public class DuplicateUsernameException : Exception
    {
        public DuplicateUsernameException() { }
        public DuplicateUsernameException(string message)
            :base(message)
        {
                
        }
    }
}
