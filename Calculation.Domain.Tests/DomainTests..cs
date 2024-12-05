using Calculation.CommonTests;
using Calculation.Domain.Model;
using Calculation.Domain.Service;
using Moq;
using static Calculation.Common.Common;

namespace Calculation.Domain.Tests
{
    [TestClass]
    public class DomainTests
    {
        private Mock<VehicleService> _vehicleServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _vehicleServiceMock = new Mock<VehicleService> { CallBase = true };  // CallBase ensures base methods are executed.
        }


        [TestMethod]
        public async Task CommonVehiclePrice()
        {
  
            VehicleService vehicleService = new VehicleService();
            var vehicle = await vehicleService.CalculateFees(1000, VehicleType.Common, CancellationToken.None);
            Utility.AssertData(vehicle, 1000, 50, 20, 10, 1180);
        }

        [TestMethod]
        public async Task LuxuryVehiclePrice()
        {

            VehicleService vehicleService = new VehicleService();
            var vehicle = await vehicleService.CalculateFees(1800, VehicleType.Luxury, CancellationToken.None);
            Utility.AssertData(vehicle, 1800, 180, 72, 15, 2167);
        }

        [TestMethod]
        public async Task PriceZeroOrNegative()
        {
            VehicleService vehicleService = new VehicleService();
            var vehicle = await vehicleService.CalculateFees(-1, VehicleType.Luxury, CancellationToken.None);
            Utility.AssertData(vehicle, 0, 0, 0, 0, 0);
        }

    }
}