namespace OmniUser.API.ViewModels;

public record ViewModel<T>
{
    public T? Dados { get; set; }
    public bool Sucesso { get; set; }
    public string? Erros { get; set; }
}
