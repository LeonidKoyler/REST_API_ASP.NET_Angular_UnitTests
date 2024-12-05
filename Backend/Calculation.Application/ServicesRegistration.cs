using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Calculation.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(ServicesRegistration));
            });
            
            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
            
            return services;
        }
    }
}
