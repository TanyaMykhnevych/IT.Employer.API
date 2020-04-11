using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IT.Employer.Services.Models.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "IT.Employer.API";
        public const string AUDIENCE = "IT.Employer";
        private const string SECRET_KEY = "4HqoFK424mTaaV3rOWq3uBy0z3JVc8Yh";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
        }
    }
}
