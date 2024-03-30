using System.Reflection;
using Microsoft.OpenApi.Models;

namespace OmniUser.API.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "OmniUser API",
                Contact = new OpenApiContact
                {
                    Name = "Brenno Martins",
                    Url = new Uri("https://eita.vix.br/eu")
                }
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }

    public static void UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "OmniUser API v1");
            options.RoutePrefix = string.Empty;
        });
    }
}
