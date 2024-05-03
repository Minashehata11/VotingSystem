using AutoMapper;
using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;
using DAL.VotingSystem.Entities.UserIdentity;

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
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.User.DateOfBirth))
                ;

            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.Name, src => src.MapFrom(a => a.User.UserName))
                .ForMember(dest => dest.SSN, src => src.MapFrom(a => a.User.SSN));
            CreateMap<Admin, AdminViewDto>().ForMember(dest => dest.Name, src => src.MapFrom(x => x.User.UserName))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.User.Email))
                .ForMember(dest => dest.SSN, src => src.MapFrom(x => x.User.SSN))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.User.City))
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.User.DateOfBirth))
                ;
            CreateMap<CreateAdminDto, ApplicationUser>();
            CreateMap<Candidate, AllCandidateDto>().ForMember(dest=>dest.FullName,src=>src.MapFrom(x=>x.User.FullName));
            CreateMap<Post, PostDtoView>()
                .ForMember(dest=>dest.FullName,src=>src.MapFrom(x=>x.Candidate.User.FullName))
                .ForMember(dest=>dest.Qualfication,src=>src.MapFrom(x=>x.Candidate.Qulification))
                ;


        }
    }
}
