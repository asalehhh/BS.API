
namespace BS.DataLayer.BusinessLayer.Repositories.UserAuthentication
{
    /// <summary>
    /// Represents interface for user authentication
    /// </summary>
    public interface IUserAuthenticationManager
    {
        Task<string> GenerateToken(string userName, string indetityId, int profileId);
    }
}
