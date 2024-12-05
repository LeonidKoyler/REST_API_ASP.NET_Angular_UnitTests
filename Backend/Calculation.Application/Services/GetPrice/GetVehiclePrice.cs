using Calculation.Domain.Model;
using MediatR;

using static Calculation.CommonData.Common;

namespace Calculation.Application.Services.GetPrice
{
    public record GetVehiclePrice(decimal basePrice, VehicleType type) : IRequest<Vehicle>;

}
