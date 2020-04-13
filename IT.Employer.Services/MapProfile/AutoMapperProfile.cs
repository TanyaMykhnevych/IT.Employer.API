using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Domain.Models.User;
using IT.Employer.Domain.Models.Vacancy;
using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Company;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Entities.Models.Team;
using IT.Employer.Entities.Models.User;
using IT.Employer.Entities.Models.Vacancy;
using IT.Employer.Services.Extensions;
using IT.Employer.Services.Models.User;

namespace IT.Employer.Services.MapProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();

            CreateMap<CreateUserModel, AppUser>()
                .IgnoreAllUnmapped()
                .ForMember(u => u.Role, m => m.MapFrom(u => u.Role))
                //.ForMember(u => u.FirstName, m => m.MapFrom(u => u.FirstName))
                //.ForMember(u => u.LastName, m => m.MapFrom(u => u.LastName))
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Username));

            CreateMap<UpdateUserModel, AppUser>()
                .IgnoreAllUnmapped()
                .ForMember(u => u.Role, m => m.MapFrom(u => u.Role))
                .ForMember(u => u.FirstName, m => m.MapFrom(u => u.FirstName))
                .ForMember(u => u.LastName, m => m.MapFrom(u => u.LastName))
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Username))
                .ForMember(u => u.IsActive, m => m.MapFrom(u => u.IsActive));


            CreateMap<BaseEntity, BaseEntityDTO>().ReverseMap();

            CreateMap<Profession, ProfessionDTO>().ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Technology, TechnologyDTO>().ReverseMap();
            CreateMap<CompanySize, CompanySizeDTO>().ReverseMap();
            CreateMap<CompanyType, CompanyTypeDTO>().ReverseMap();

            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Characteristic, CharacteristicDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Vacancy, VacancyDTO>().ReverseMap();
        }
    }
}
