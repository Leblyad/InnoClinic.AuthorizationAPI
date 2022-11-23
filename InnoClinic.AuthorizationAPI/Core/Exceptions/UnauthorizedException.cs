namespace InnoClinic.AuthorizationAPI.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Unauthorized user.")
        {
        }
    }
}
