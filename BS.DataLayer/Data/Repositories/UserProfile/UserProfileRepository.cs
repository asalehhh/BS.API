using BS.DataLayer.Models.User;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;


namespace BS.DataLayer.Data.Repositories.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserProfileRepository> logger;
        private readonly DapperContext dpContext;
        public UserProfileRepository(ApplicationDbContext dbContext, ILogger<UserProfileRepository> logger, DapperContext dpContext)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.dpContext = dpContext;
        }

        public async Task<Models.User.UserProfile> GetByIdentityId(string identityId)
        {
            Models.User.UserProfile user = null;
            try
            {
                user = await this.dbContext.UserProfile.FirstOrDefaultAsync(c => c.IdentityUserId == identityId);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(GetByIdentityId)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
            }

            return user;
        }

        public async Task<UserProfileInfo> GetUserProfile(int userProfileId)
        {
            UserProfileInfo user = null;
            try
            {
                using (var connection = dpContext.CreateConnection())
                {
                    //Set up DynamicParameters object to pass parameters  
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserProfileId", userProfileId);

                    var result = connection.QuerySingleOrDefault<UserProfileInfo>("GetUserProfile", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(GetUserProfile)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
            }

            return user;
        }
    }
}
