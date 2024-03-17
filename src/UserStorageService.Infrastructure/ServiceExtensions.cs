using System.Reflection;
using Common.PipelineBehaviours;
using Domain.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserStorageService.Infrastructure.Repositories;

namespace UserStorageService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.UsingRabbitMq((context, cfg) =>
                {
                    var connectionString = configuration.GetConnectionString("RabbitMQ");
                    cfg.Host(connectionString);
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        }
    }
}