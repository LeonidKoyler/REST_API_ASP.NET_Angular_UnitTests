using Calculation.Domain.Model;
using static Calculation.CommonData.Common;

namespace Calculation.Application.Services
{
    public interface IVehicleService2
    {
        Task<Vehicle> CalculateFees(decimal basePrice, VehicleType type, CancellationToken token = default);
    }
}
