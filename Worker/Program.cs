using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var endpointConfiguration = new EndpointConfiguration("Worker");
                    endpointConfiguration.UseTransport<LearningTransport>();
                    endpointConfiguration.UseContainer<ServicesBuilder>(
                        customizations: customizations =>
                        {
                            customizations.ExistingServices(services);
                        });

                    services.AddSingleton(endpointConfiguration);
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IEndpointInstance>(sp => sp.GetRequiredService<Worker>().EndpointInstance);
                });
    }
}
