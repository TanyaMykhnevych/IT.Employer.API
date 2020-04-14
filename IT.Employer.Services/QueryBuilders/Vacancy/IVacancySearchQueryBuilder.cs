using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.VacancyN;
using IT.Employer.Services.QueryBuilders.Base;

namespace IT.Employer.Services.QueryBuilders.VacancyN
{
    public interface IVacancySearchQueryBuilder : IQueryBuilder<Vacancy>
    {
        IVacancySearchQueryBuilder SetBaseVacanciesInfo(bool asNoTracking = true, bool includeRelated = false);
        IVacancySearchQueryBuilder SetProfession(Profession? profession);
        IVacancySearchQueryBuilder SetPosition(Position? position);
        IVacancySearchQueryBuilder SetPrimaryTechnology(Technology? technology);
        IVacancySearchQueryBuilder SetExperience(int? from, int? to);
    }
}
