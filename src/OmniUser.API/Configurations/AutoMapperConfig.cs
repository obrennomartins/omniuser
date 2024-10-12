using AutoMapper;
using OmniUser.API.ViewModels;
using OmniUser.Domain.Models;

namespace OmniUser.API.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
    }
}
