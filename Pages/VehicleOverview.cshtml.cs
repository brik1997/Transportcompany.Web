using Microsoft.AspNetCore.Mvc.RazorPages;
using TransportCompany.Data;

//haalt de voertuigen op uit de database en toont ze in een overzichtspagina
namespace TransportCompany.Web.Pages
{
    public class VehicleOverviewModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<Vehicle> Vehicles { get; set; } = new();

        public VehicleOverviewModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            VehicleRepository vehicleRepository = new VehicleRepository(connectionString);
            Vehicles = vehicleRepository.GetAllVehicles();


        }
    }
}