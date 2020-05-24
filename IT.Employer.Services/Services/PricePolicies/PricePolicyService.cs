using IT.Employer.Services.Models.Settings;
using IT.Employer.Services.Models.Settings.Price;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace IT.Employer.Services.Services.PricePolicies
{
    public class PricePolicyService : IPricePolicyService
    {
        private readonly AppSettings _appSettings;

        public PricePolicyService(IOptionsMonitor<AppSettings> appSettingsOptions)
        {
            _appSettings = appSettingsOptions.CurrentValue;
        }

        public decimal CalculateHiringHourPrice(decimal hourRate, int teamSize)
        {
            double extraChargeCoefficient = GetExtraChargeCoefficient(teamSize);

            return GetPriceWithExtraCharge(hourRate, extraChargeCoefficient);
        }

        public decimal CalculateTeamHiringHourPrice(decimal[] hourRates)
        {
            double extraChargeCoefficient = GetExtraChargeCoefficient(hourRates.Length);
            decimal sumHourRate = hourRates.Sum();

            return GetPriceWithExtraCharge(sumHourRate, extraChargeCoefficient);
        }

        public decimal CalculateInitialHourPrice(decimal hiringHourRate, int teamSize)
        {
            double extraChargeCoefficient = GetExtraChargeCoefficient(teamSize);

            return Math.Round(hiringHourRate / (decimal)(1 + extraChargeCoefficient));
        }

        public double GetExtraChargeCoefficient(int teamSize)
        {
            foreach (PricePolicy pricePolicy in _appSettings.PricePolicies.OrderByDescending(p => p.MinMembersNumber))
            {
                if (teamSize >= pricePolicy.MinMembersNumber)
                {
                    return pricePolicy.ExtraChargeCoefficient;
                }
            }

            return 0;
        }

        private decimal GetPriceWithExtraCharge(decimal hourRate, double extraChargeCoefficient)
        {
            return Math.Round(hourRate * (decimal)(1 + extraChargeCoefficient));
        }
    }
}
