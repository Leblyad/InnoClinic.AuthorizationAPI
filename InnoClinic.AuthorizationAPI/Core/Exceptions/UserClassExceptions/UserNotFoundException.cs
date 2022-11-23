namespace InnoClinic.AuthorizationAPI.Core.Exceptions.UserClassExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string login) : base($"The user with the login {login} was not found.")
        {
        }
    }
}
