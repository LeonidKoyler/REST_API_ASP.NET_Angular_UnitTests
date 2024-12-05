using Calculation.Domain.Model;
using Calculation.Domain.Service;
using FluentValidation;
using MediatR;

namespace Calculation.Application.Services.GetPrice
{
    public class GetVehiclePriceHandler : IRequestHandler<GetVehiclePrice, Vehicle>
    {
        private IVehicleService _vehicleService;
        private readonly IValidator<Vehicle> _vehicleValidator;
        public GetVehiclePriceHandler(IVehicleService vehicleService, IValidator<Vehicle> vehicleValidator)
        {
            _vehicleService = vehicleService;
            _vehicleValidator = vehicleValidator;
        }

        public async Task<Vehicle> Handle(GetVehiclePrice request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle()
            {
                BasePrice = request.basePrice,
                Type = request.type
            };
            await _vehicleValidator.ValidateAndThrowAsync(vehicle, cancellationToken);
            return await _vehicleService.CalculateFees(request.basePrice, request.type, cancellationToken);
        }
    }
}
