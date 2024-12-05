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

        IVehicleService _vehicleCalculator;
        public PriceController(ISender vehicleMediator, ILogger<PriceController> logger)
        {
            _logger = logger;
            _vehicleMediator = vehicleMediator;
        }

        [HttpPost(ApiEndpoints.CalculatePrice)]
        public async Task<IActionResult> Price([FromBody] CalculationRequest request, CancellationToken token)
        {
            // todo should remove duplication of vehicleType
            VehicleType vehicleType;
            Enum.TryParse(request.Type, true, out vehicleType);
            var updatedPrice = await _vehicleMediator.Send(new GetVehiclePrice(request.BasePrice, vehicleType));
            var response = updatedPrice.MapToVehicleResponse();
            return Ok(response);
        }
    }
}
