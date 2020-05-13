namespace IT.Employer.Services.Services.PricePolicies
{
    public interface IPricePolicyService
    {
        decimal CalculateHiringHourPrice(decimal hourPrice, int teamSize);
        double GetExtraChargeCoefficient(int teamSize);
    }
}
