using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Worker.Messages;

namespace Worker.Handlers
{
    public class MyMessageHandler : IHandleMessages<MyMessage>
    {
        private readonly ILogger<MyMessageHandler> _logger;

        public MyMessageHandler(ILogger<MyMessageHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received a message");
            return Task.CompletedTask;
        }
    }
}