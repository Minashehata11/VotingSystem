using Microsoft.OpenApi.Models;

namespace PL.VotingSystem.Extentions
{
    public static class SwaggerServiceExtinctions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Voting System", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }

                };
                option.AddSecurityDefinition("bearer", securitySchema);
                var securityRequrment = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "bearer" } }
                };
                option.AddSecurityRequirement(securityRequrment);
            });
            return services;



        }
    }
}
