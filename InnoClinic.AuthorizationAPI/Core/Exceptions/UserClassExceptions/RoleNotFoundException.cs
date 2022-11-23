namespace InnoClinic.AuthorizationAPI.Core.Exceptions.UserClassExceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string role) : base($"The role with the name {role} was not found.")
        {
        }
    }
}
