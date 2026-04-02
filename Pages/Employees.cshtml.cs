using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TransportCompany.Data;
using TransportCompany.Logic;

namespace TransportCompany.Web.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Brand { get; set; } = string.Empty;

        [BindProperty]
        public string ModelName { get; set; } = string.Empty;

        [BindProperty]
        public int BuildYear { get; set; }

        [BindProperty]
        public decimal PurchasePrice { get; set; }
       
        [BindProperty]
        public decimal Capacity { get; set; }

        [BindProperty]
        public int Mileage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            Vehicle vehicle = new Vehicle
            {
                Brand = Brand,
                ModelName = ModelName,
                BuildYear = BuildYear,
                PurchasePrice = PurchasePrice,
                Mileage = Mileage
            };

            VehicleService vehicleService = new VehicleService(connectionString);
            vehicleService.AddVehicle(vehicle);

            return RedirectToPage();
        }
    }
}