using BLL.VotingSystem.Repository;
using BLL.VotingSystem.Services;
using DAL.VotingSystem.Entities.UserIdentity;
using LearnApi.HelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PL.VotingSystem.Extentions
{
    public static class ApplicationServiceExtinctions
    {
        public static IServiceCollection AddServiceExtintions(this IServiceCollection services)
        {
            services.AddAutoMapper(map => map.AddProfile(new MapperProfile()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFlaskConsumer, FlaskConsumer>();



            return services;
        }
    }
}
