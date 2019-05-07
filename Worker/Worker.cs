using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Worker.Messages;

namespace Worker
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly EndpointConfiguration _endpointConfiguration;
        public IEndpointInstance EndpointInstance;

        public Worker(ILogger<Worker> logger, EndpointConfiguration endpointConfiguration)
        {
            _logger = logger;
            _endpointConfiguration = endpointConfiguration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            EndpointInstance = await Endpoint.Start(_endpointConfiguration);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await EndpointInstance.Stop();
        }
    }
}
