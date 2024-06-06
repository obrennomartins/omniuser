using Moq.AutoMock;

namespace OmniUser.Tests.Services;

public abstract class ServiceBaseArrangements
{
    public AutoMocker Mocker { get; set; }
}
