using IT.Employer.Services.Factories.AuthTokenFactory;
using IT.Employer.Services.Models.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.UserAuthorizationService
{
    public abstract class BaseAuthorizationService
    {
        private readonly IAuthTokenFactory _tokenFactory;
        public BaseAuthorizationService(IAuthTokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }
        public async Task<JWTTokenStatusResult> GenerateTokenAsync(AuthSignInModel model)
        {
            bool status = await VerifyUserAsync(model);
            if (!status)
            {
                return new JWTTokenStatusResult() { Token = null, IsAuthorized = false };
            }

            IEnumerable<Claim> claims = await GetUserClaimsAsync(model);
            JwtSecurityToken token = _tokenFactory.CreateToken(model.UserName.ToString(), claims);
            return new JWTTokenStatusResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsAuthorized = true,
            };
        }

        public abstract Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model);
        public abstract Task<bool> VerifyUserAsync(AuthSignInModel model);
    }
}
