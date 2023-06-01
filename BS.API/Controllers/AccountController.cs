using BS.DataLayer.Data.Repositories.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountRepository accountRepository;
        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
        }


        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<ActionResult> CreateAccount([FromBody]decimal initialBalance)
        {
            try
            {
                //get profile id from token
                var profileId = User.FindFirst("ProfileId").Value;

                var createResult = await this.accountRepository.CreateAccount(int.Parse(profileId), initialBalance);

                return Ok(createResult);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(CreateAccount)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("delete")]
        public async Task<ActionResult> DeleteAccount()
        {
            try
            {
                //get profile id from token
                var profileId = User.FindFirst("ProfileId").Value;

                var createResult = await this.accountRepository.RemoveAccount(int.Parse(profileId));

                return Ok(createResult);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(DeleteAccount)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

