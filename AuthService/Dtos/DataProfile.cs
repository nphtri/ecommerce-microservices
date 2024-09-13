using AuthService.Models;
using AutoMapper;

namespace AuthService.Dtos
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Account, AccountReadDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<Role, RoleReadDto>();
        }
    }
}