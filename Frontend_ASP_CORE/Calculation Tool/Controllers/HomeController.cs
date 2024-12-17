using Calculation_Tool.Models;
using Calculation_Tool.Service;
using Microsoft.AspNetCore.Mvc;


namespace Calculation_Tool.Controllers
{
    public class HomeController : Controller
    {
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
            if (ModelState.IsValid && model.BasePrice > 0)
            {
                model.Type = model.VehicleType.ToString();
                model = await _apiService.PostDataAsync(model);
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
