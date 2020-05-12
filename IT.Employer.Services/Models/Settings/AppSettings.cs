using IT.Employer.Services.Models.Settings.Price;

namespace IT.Employer.Services.Models.Settings
{
    public class AppSettings
    {
        public PricePolicy[] PricePolicies { get; set; } = new PricePolicy[0];
    }
}
