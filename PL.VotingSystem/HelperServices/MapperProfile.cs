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
        }
    }
}
