using Microsoft.OpenApi.Models;

namespace BS.API.Infrastructure
{
    public class SwaggerConfig
    {
        public static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BS API",
                    Version = "v1",
                    Description = "BS API Services.",
                    Contact = new OpenApiContact
                    {
                        Name = "BS"
                    },
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });

            services.AddSwaggerGen();
        }
    }
}
