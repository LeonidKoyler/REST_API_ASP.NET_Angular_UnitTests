using Calculation.API.Mapping;
using Calculation.Application.Services.GetPrice;
using Calculation.Contract.Request;
using Calculation.Domain.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Calculation.CommonData.Common;

namespace Calculation.API.Controllers
{
    [ApiController]

    public class PriceController : ControllerBase
    {

        private readonly ILogger<PriceController> _logger;
        private readonly ISender _vehicleMediator;

        public PriceController(ISender vehicleMediator, ILogger<PriceController> logger)
        {
            _logger = logger;
            _vehicleMediator = vehicleMediator;
        }

        [HttpPost(ApiEndpoints.CalculatePrice)]
        public async Task<IActionResult> Price([FromBody] CalculationRequest request, CancellationToken token)
        {
            // todo should remove duplication of vehicleType
            if (!Enum.TryParse<VehicleType>(request.Type, true, out var vehicleType))
            {
                throw new ArgumentException($"Invalid vehicle type: {request.Type}");
            }

            var updatedPrice = await _vehicleMediator.Send(new GetVehiclePrice(request.BasePrice, vehicleType), token);
            var response = updatedPrice.MapToVehicleResponse();
            return Ok(response);
        }
    }
}
