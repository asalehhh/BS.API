using BS.DataLayer.Data.Repositories.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly ITransactionRepository transactionRepository;
        public TransactionController(ILogger<AccountController> logger, ITransactionRepository transactionRepository)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
        }


        [HttpGet]
        [Authorize]
        [Route("get")]
        public async Task<ActionResult> GetAccountTransactions()
        {
            try
            {
                //get profile id from token
                var profileId = User.FindFirst("ProfileId").Value;

                var transactions = await this.transactionRepository.GetAccountTransactions(int.Parse(profileId));

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(GetAccountTransactions)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

