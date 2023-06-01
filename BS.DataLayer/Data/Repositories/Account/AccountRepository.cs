using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BS.DataLayer.Data.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> logger;
        private readonly DapperContext dpContext;
        public AccountRepository(ILogger<AccountRepository> logger, DapperContext dpContext)
        {
            this.logger = logger;
            this.dpContext = dpContext;
        }

        public async Task<bool> CreateAccount(int userProfileId, decimal initialBalance)
        {
            var success = true;
            try
            {
                using (var connection = dpContext.CreateConnection())
                {
                    //Set up DynamicParameters object to pass parameters  
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserProfileId", userProfileId);
                    parameters.Add("@InitialBalance", initialBalance);

                    var result = await connection.ExecuteAsync("CreateAccount", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                success = false;
                this.logger.LogError($"{nameof(CreateAccount)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
            }

            return success;
        }

        public async Task<bool> RemoveAccount(int userProfileId)
        {
            var success = true;
            try
            {
                using (var connection = dpContext.CreateConnection())
                {
                    //Set up DynamicParameters object to pass parameters  
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserProfileId", userProfileId);

                    var result = await connection.ExecuteAsync("ResetAccount", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                success = false;
                this.logger.LogError($"{nameof(RemoveAccount)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
            }

            return success;
        }
    }
}
