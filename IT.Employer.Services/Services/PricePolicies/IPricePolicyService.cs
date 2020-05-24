namespace IT.Employer.Services.Services.PricePolicies
{
    public interface IPricePolicyService
    {
        decimal CalculateHiringHourPrice(decimal hourPrice, int teamSize);
        decimal CalculateTeamHiringHourPrice(decimal[] hourRates);
        decimal CalculateInitialHourPrice(decimal hiringHourRate, int teamSize);
        double GetExtraChargeCoefficient(int teamSize);
    }
}
