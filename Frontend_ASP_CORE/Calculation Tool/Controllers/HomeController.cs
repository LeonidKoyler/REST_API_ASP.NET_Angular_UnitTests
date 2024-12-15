using Calculation_Tool.Models;
using Calculation_Tool.Service;
using Microsoft.AspNetCore.Mvc;


namespace Calculation_Tool.Controllers
{
    public class HomeController : Controller
    {
        private string apiLocalUrl = $"https://localhost:7198/api/vehicle";
        private string apiDockerUrl = $"http://backend/api/vehicle";

        private readonly ILogger<HomeController> _logger;
        private static List<VehicleModel> _history = new List<VehicleModel>();
        private IApiService _apiService;
        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            var model = new VehicleModel();
            ViewBag.BasePrice = model.BasePrice;
            return View(model);
        }

        [Route("/requirements")]
        public IActionResult Requirements()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CalculateFees(VehicleModel model)
        {
            //  todoGetting url should be implemented in different way.
            // This is temporary solution for supporting docker containers and local run from Visual studio.
            var url = string.Empty;
            var isRunningFromVisualStudio = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == null &&
                                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            
            url = isRunningFromVisualStudio ? $"https://localhost:7198/api/vehicle" : $"http://backend/api/vehicle";


            if (ModelState.IsValid && model.BasePrice > 0)
            {
                model.Type = model.VehicleType.ToString();
                model = await _apiService.PostDataAsync(url, model);
                if (model != null)
                {
                    _history.Add(model);
                    model.History = _history;
                    ViewBag.BasePrice = model.BasePrice;
                }
                else
                {
                    throw new Exception("Can't communicate to API");
                }
            }

            return View("Index", model);
        }
    }
}
