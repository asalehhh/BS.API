using BS.DataLayer.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BS.DataLayer.Data.Repositories.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TransactionRepository> logger;
        public TransactionRepository(ApplicationDbContext dbContext, ILogger<TransactionRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<AccountTransaction>> GetAccountTransactions(int userProfileId)
        {
            List<AccountTransaction> transactions = null;
            try
            {
                transactions = await this.dbContext.AccountTransactions.Where(c => c.UserAccount.UserProfileId == userProfileId).ToListAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(GetAccountTransactions)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
            }

            return transactions;
        }
    }
}
