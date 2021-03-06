﻿using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.Base;
using IT.Employer.Domain.Models.CompanyN;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.Hiring;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Domain.Models.User;
using IT.Employer.Domain.Models.VacancyN;
using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Entities.Models.Hiring;
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
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(e => e.Team, opts => opts.Ignore())
                .ForMember(e => e.CompanyName, opts => opts.MapFrom(s => s.Company.Name));
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(e => e.Team, opts => opts.Ignore())
                .ForMember(e => e.Company, opts => opts.Ignore());
            CreateMap<Characteristic, CharacteristicDTO>().ForMember(c => c.Employee, opts => opts.Ignore()).ReverseMap();
            CreateMap<Vacancy, VacancyDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Hire, HireDTO>().ReverseMap();
        }
    }
}
