using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace OmniUser.Jobs;

public static class Functions
{
    public static void ProcessQueueMessage([QueueTrigger("queue")] string message, ILogger logger)
    {
        logger.LogInformation(message);
    }
}
