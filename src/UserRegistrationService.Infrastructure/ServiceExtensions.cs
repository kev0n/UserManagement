using Common.PipelineBehaviours;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserRegistrationService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var connectionString = configuration.GetConnectionString("RabbitMQ");
                    cfg.Host(connectionString);
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}