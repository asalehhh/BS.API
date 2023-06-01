
namespace BS.DataLayer.Data.Repositories.Account
{
    public interface IAccountRepository
    {
        Task<bool> CreateAccount(int userProfileId, decimal initialBalance);

        Task<bool> RemoveAccount(int userProfileId);
    }
}
