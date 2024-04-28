using AutoMapper;
using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;

namespace LearnApi.HelperServices
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Voter, UsersDto>().ForMember(dest=>dest.Name,src=>src.MapFrom(x=>x.User.UserName))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(x => x.Category.Name))
                .ForMember(dest => dest.SSN, src => src.MapFrom(x => x.User.SSN));
            CreateMap<Voter, UserViewDto>().ForMember(dest => dest.Name, src => src.MapFrom(x => x.User.UserName))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.User.Email))
                .ForMember(dest => dest.SSN, src => src.MapFrom(x => x.User.SSN))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.User.City))
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.User.DateOfBirth));
                
                
        }
    }
}
