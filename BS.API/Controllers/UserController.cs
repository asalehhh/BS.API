using BS.DataLayer.BusinessLayer.Repositories.UserAuthentication;
using BS.DataLayer.Data.Repositories.UserProfile;
using BS.DataLayer.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<UserController> logger;
        private readonly IUserAuthenticationManager userAuthenticationRepository;
        private readonly IUserProfileRepository userProfileRepository;
        public UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager, IUserAuthenticationManager userAuthenticationRepository, IUserProfileRepository userProfileRepository)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.userAuthenticationRepository = userAuthenticationRepository;
            this.userProfileRepository = userProfileRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login(Login userLogin)
        {
            try
            {
                var identityUser = await this.userManager.FindByNameAsync(userLogin.UserName);
                if (identityUser != null)
                {
                    var userProfile = await this.userProfileRepository.GetByIdentityId(identityUser.Id);

                    if (identityUser != null && userProfile != null)
                    {
                        var validCredentials = await userManager.CheckPasswordAsync(identityUser, userLogin.Password);
                        if (validCredentials)
                        {
                            var token = await userAuthenticationRepository.GenerateToken(userLogin.UserName, identityUser.Id, userProfile.Id);
                            return Ok(new { token = token });
                        }
                    }
                }
                else
                {
                    return BadRequest("Invalid user");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(Login)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest("Failed to login");
        }


        [HttpGet]
        [Authorize]
        [Route("get/profile")]
        public async Task<ActionResult> GetUserProfile()
        {
            try
            {
                //get profile id from token
                var profileId = User.FindFirst("ProfileId").Value;

                var userProfileIfo = await this.userProfileRepository.GetUserProfile(int.Parse(profileId));

                return Ok(userProfileIfo);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(GetUserProfile)} api encountered an error. Message : {ex.Message}, Stack Trace :  {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

