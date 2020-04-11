namespace IT.Employer.Services.Models.Auth
{
    public class JWTTokenStatusResult
    {
        public string Token { get; internal set; }
        public bool IsAuthorized { get; internal set; }
    }
}
