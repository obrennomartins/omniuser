using NetArchTest.Rules;

namespace OmniUser.Tests;

public class Architecture
{
    [Fact(DisplayName = "Domínio não deve ter dependência em API")]
    [Trait("Category", "Arquitetura")]
    public void CamadaDomain_NaoDeveTerDependenciaEm_CamadaApi()
    {
        // Arrange
        var domainAssembly = typeof(Domain.Models.EntidadeBase).Assembly;
        var apiAssembly = typeof(API.Configurations.InjecaoDeDependenciasConfig).Assembly;

        // Act
        var resultado = Types.InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOn(apiAssembly.GetName().Name)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "Infraestrutura não deve ter dependência em API")]
    [Trait("Category", "Arquitetura")]
    public void CamadaInfrastructure_NaoDeveTerDependenciaEm_CamadaApi()
    {
        // Arrange
        var infrastructureAssembly = typeof(Infrastructure.Session.OmniUserDbSession).Assembly;
        var apiAssembly = typeof(API.Configurations.InjecaoDeDependenciasConfig).Assembly;

        // Act
        var resultado = Types.InAssembly(infrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(apiAssembly.GetName().Name)
            .GetResult()
            .IsSuccessful;

        // Assert
        Assert.True(resultado);
    }
}
