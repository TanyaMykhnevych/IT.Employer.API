using AutoMapper;
using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.User;
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
        }
    }
}
