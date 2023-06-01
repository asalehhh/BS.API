using BS.DataLayer.Models.User;

namespace BS.DataLayer.Data.Repositories.UserProfile
{
    public interface IUserProfileRepository
    {
        Task<Models.User.UserProfile> GetByIdentityId(string identityId);
        Task<UserProfileInfo> GetUserProfile(int userProfileId);
    }
}
