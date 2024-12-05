using System.Net;
using System.Net.Http.Json;
using Calculation.CommonTests;
using Calculation_Tool.Models;
using Newtonsoft.Json;


namespace Calculation.Test
{
    [TestClass]
    public class CalculationTest
    {
        VehicleModel? responceModel = null;

        private string apiUrl = $"https://localhost:7198/api/vehicle";

        [TestInitialize]
        public void Init()
        {
            responceModel = null;
        }

        [TestMethod]
        public async Task VehicleInvalidType()
        {
            HttpClient httpclient = new HttpClient();

            var model = new VehicleModel
            {
                BasePrice = -1,
                BasicBuyerFee = 0m,
                AssociationCost = 0m,
                TotalCost = 0m,
                SellersSpecialFee = 0m,
                Type = "InvalidType",
                VehicleType = VehicleType.Common,
                StorageFee = 0m
            };

            var jsonContent = JsonConvert.SerializeObject(model);
            var response = await httpclient.PostAsJsonAsync(apiUrl, model);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public async Task PriceZeroOrNegative()
        {
            HttpClient httpclient = new HttpClient();

            var model = new VehicleModel
            {
                BasePrice = -1,
                BasicBuyerFee = 0m,
                AssociationCost = 0m,
                TotalCost = 0m,
                SellersSpecialFee = 0m,
                Type = "common",
                VehicleType = VehicleType.Common,
                StorageFee = 0m
            };

            var responceModel = await SendRequest(httpclient, model);

            Utility.AssertFrontEnd(responceModel, 0, 0, 0, 0,  0);

        }


        [TestMethod]
        public async Task CommonVehiclePrice()
        {
            HttpClient httpclient = new HttpClient();

            var model = new VehicleModel
            {
                BasePrice = 1000m,
                BasicBuyerFee = 100m,
                AssociationCost = 10m,
                TotalCost = 1210m,
                SellersSpecialFee = 40m,
                Type = "common",
                VehicleType = VehicleType.Common,
                StorageFee = 100m
            };

            var responceModel = await SendRequest(httpclient, model);

            Utility.AssertFrontEnd(responceModel, 1000, 50, 20, 10, 1180);

        }

        [TestMethod]
        public async Task LuxuryVehiclePrice()
        {
            HttpClient httpclient = new HttpClient();

            var model = new VehicleModel
            {
                BasePrice = 1800m,
                BasicBuyerFee = 180m,
                AssociationCost = 15m,
                TotalCost = 2167m,
                SellersSpecialFee = 72m,
                Type = "Luxury",
                VehicleType = VehicleType.Luxury,
                StorageFee = 100m
            };

            var responceModel = await SendRequest(httpclient, model);

            Utility.AssertFrontEnd(responceModel, 1800, 180, 72, 15, 2167);

        }

        private async Task<VehicleModel?> SendRequest(HttpClient httpclient, VehicleModel model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var response = await httpclient.PostAsJsonAsync(apiUrl, model);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VehicleModel>(apiResponse);
        }

     
    }
}