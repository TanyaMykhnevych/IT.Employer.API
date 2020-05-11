using IT.Employer.Services.Models.Auth;
using IT.Employer.Services.Services.UserAuthorizationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BaseAuthorizationService _authorizationService;
        public AuthController(BaseAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody]AuthSignInModel model)
        {
            JWTTokenStatusResult result = await _authorizationService.GenerateTokenAsync(model);
            if (!result.IsAuthorized) { return NotFound(); }

            return Ok(result);
        }

        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            string currentUserName = User.Identity.Name;
            UserAuthInfo userInfo = await _authorizationService.GetUserInfoAsync(currentUserName);

            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }
    }
}
