using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Services.QueryBuilders.Base;
using System;

namespace IT.Employer.Services.QueryBuilders.EmployeeN
{
    public interface IEmployeeSearchQueryBuilder : IQueryBuilder<Employee>
    {
        IEmployeeSearchQueryBuilder SetBaseEmployeesInfo(bool asNoTracking = true, bool includeRelated = false);
        IEmployeeSearchQueryBuilder SetFirstName(string firstName);
        IEmployeeSearchQueryBuilder SetLastName(string lastName);
        IEmployeeSearchQueryBuilder SetProfession(Profession? profession);
        IEmployeeSearchQueryBuilder SetPosition(Position? position);
        IEmployeeSearchQueryBuilder SetPrimaryTechnology(Technology? technology);
        IEmployeeSearchQueryBuilder SetExperience(int? from, int? to);
        IEmployeeSearchQueryBuilder SetCompanyId(Guid? companyId);
        IEmployeeSearchQueryBuilder SetTeamId(Guid? teamId);
    }
}
