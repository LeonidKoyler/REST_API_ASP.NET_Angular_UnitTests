using Calculation.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation.Infrastructure.Calculation;
using Calculation.Domain.Service;

namespace Calculation.Infrastructure
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<IVehicleService, Domain.Service.VehicleService>();
        }
    }
}
