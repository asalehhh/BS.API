using BS.DataLayer.Models.Transactions;

namespace BS.DataLayer.Data.Repositories.Transaction
{
    public interface ITransactionRepository
    {
        Task<List<AccountTransaction>> GetAccountTransactions(int userProfileId);
    }
}
