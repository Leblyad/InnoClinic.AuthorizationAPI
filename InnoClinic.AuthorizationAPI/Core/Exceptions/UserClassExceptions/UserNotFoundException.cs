namespace InnoClinic.AuthorizationAPI.Core.Exceptions.UserClassExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string email) : base($"The user with the login {email} was not found.")
        {
        }
    }
}
