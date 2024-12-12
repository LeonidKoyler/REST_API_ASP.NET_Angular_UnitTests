using Calculation.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.Domain
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services.AddScoped<IVehicleService, VehicleService>();
        }
    }
}
