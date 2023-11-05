using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Notificacoes;
using OmniUser.Domain.Services;
using OmniUser.Infrastructure.Repositories;
using OmniUser.Infrastructure.ViaCep;

namespace OmniUser.API.Configurations;

public static class InjecaoDeDependenciasConfig
{
    public static void RegistrarDependencias(this IServiceCollection services)
    {
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        services.AddScoped<IRegistroAuditoriaRepository, RegistroAuditoriaRepository>();

        services.AddHttpClient<IViaCepRepository, ViaCepRepository>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler(PollyConfig.GetRetryPolicy());
    }
}