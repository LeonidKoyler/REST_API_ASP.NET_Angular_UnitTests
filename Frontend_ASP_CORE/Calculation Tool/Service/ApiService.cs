using Calculation_Tool.Controllers;
using Calculation_Tool.Models;
using Newtonsoft.Json;

namespace Calculation_Tool.Service
{
    public class ApiService : IApiService
    {
     
       private readonly IHttpClientFactory _httpClientFactory;
       private readonly ILogger<HomeController> _logger;

        public ApiService(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
      
        public async Task<VehicleModel> PostDataAsync(VehicleModel model)
        {
            VehicleModel? responceModel=null;
            var jsonContent = JsonConvert.SerializeObject(model);
            try
            {
                var client=_httpClientFactory.CreateClient("vehicleApi");
                var response = await client.PostAsJsonAsync(client.BaseAddress, model);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    responceModel = JsonConvert.DeserializeObject<VehicleModel>(apiResponse);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return responceModel;
        }
    }
}

