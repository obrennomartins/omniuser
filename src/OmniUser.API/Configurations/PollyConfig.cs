using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace OmniUser.API.Configurations;

public static class PollyConfig
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                                                  TimeSpan.FromMilliseconds(new Random().Next(0, 1000)));
    }
}