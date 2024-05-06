using BLL.VotingSystem.Repository;
using BLL.VotingSystem.Services;
using LearnApi.HelperServices;

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
            services.AddScoped<IFilerFunction, FilterFunction>();

            return services;
        }
    }
}
