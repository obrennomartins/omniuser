using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Notificacoes;
using OmniUser.Domain.Services;
using OmniUser.Infrastructure.Session;
using OmniUser.Infrastructure.Repositories;
using OmniUser.Infrastructure.UnitOfWork;
using OmniUser.Infrastructure.ViaCep;

namespace OmniUser.API.Configurations;

public static class InjecaoDeDependenciasConfig
{
    public static void RegistrarDependencias(this IServiceCollection services)
    {
        services.AddScoped<OmniUserDbSession>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<INotificador, Notificador>();

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        services.AddHttpClient<IViaCepRepository, ViaCepRepository>(client =>
        {
            client.BaseAddress = new Uri("https://viacep.com.br/");
        }).AddStandardResilienceHandler();
        services.AddScoped<IViaCepService, ViaCepService>();
    }
}
