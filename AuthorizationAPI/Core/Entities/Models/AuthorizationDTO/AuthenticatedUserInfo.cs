namespace AuthorizationAPI.Core.Entities.Models.AuthorizationDTO
{
    public class AuthenticatedUserInfo
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public IList<string> UserRoles { get; set; }
    }
}
