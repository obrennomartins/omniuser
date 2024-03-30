using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder();

builder.UseEnvironment(EnvironmentName.Development);
builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageBlobs();
    b.AddAzureStorageQueues();
});
builder.ConfigureLogging((context, b) => { b.AddConsole(); });


var host = builder.Build();

using (host)
{
    await host.RunAsync();
}
