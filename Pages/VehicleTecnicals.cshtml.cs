using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using TransportCompany.Data;
using TransportCompany.Logic;
using Microsoft.AspNetCore.Mvc;

namespace TransportCompany.Web.Pages
{
    public class VehicleTechnicalsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<Aanvraag> Aanvragen { get; set; } = new();
        public List<Vehicle> Vehicles { get; set; } = new();

        public VehicleTechnicalsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<VehicleTechnicalItem> TechnicalItems { get; set; } = new();

        public class VehicleTechnicalItem
        {
            public Aanvraag Aanvraag { get; set; }
            public Vehicle Vehicle { get; set; }
            public decimal DepreciationAmount { get; set; }
            public int DepreciationPercentage { get; set; }
            public decimal CurrentValue { get; set; }
        }



        private decimal CalculateDepreciationAmount(Vehicle vehicle)
        {
            int depreciationSteps = vehicle.Mileage / 2000;
            decimal depreciationPercentage = depreciationSteps;
            decimal depreciationAmount = vehicle.PurchasePrice * (depreciationPercentage / 100);

            return depreciationAmount;
        }

        private int CalculateDepreciationPercentage(Vehicle vehicle)
        {
            return vehicle.Mileage / 2000;
        }
        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            VehicleService vehicleService = new VehicleService(connectionString);

            List<Aanvraag> allAanvragen = aanvraagService.GetAllAanvragen();
            List<Vehicle> allVehicles = vehicleService.GetAllVehicles();

            Aanvragen = allAanvragen
                .Where(a => a.Status == "Completed")
                .ToList();

            Vehicles = allVehicles;

            List<Aanvraag> completedAanvragen = allAanvragen
                .Where(a => a.Status == "Completed" && a.VehicleId.HasValue)
                .ToList();

            foreach (Aanvraag aanvraag in completedAanvragen)
            {
                Vehicle? vehicle = allVehicles.FirstOrDefault(v => v.Id == aanvraag.VehicleId.Value);

                if (vehicle != null)
                {
                    TechnicalItems.Add(new VehicleTechnicalItem
                    {
                        Aanvraag = aanvraag,
                        Vehicle = vehicle,
                        DepreciationAmount = CalculateDepreciationAmount(vehicle),
                        DepreciationPercentage = CalculateDepreciationPercentage(vehicle),
                        CurrentValue = CalculateCurrentValue(vehicle)
                    });
                }
            }
        }

        private decimal CalculateCurrentValue(Vehicle vehicle)
        {
            decimal depreciationAmount = CalculateDepreciationAmount(vehicle);
            decimal currentValue = vehicle.PurchasePrice - depreciationAmount;

            return currentValue;
        }
        public IActionResult OnPostUpdateMileage(int aanvraagId)
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            VehicleService vehicleService = new VehicleService(connectionString);

            Aanvraag? aanvraag = aanvraagService.GetAllAanvragen()
                .FirstOrDefault(a => a.Id == aanvraagId);

            if (aanvraag == null || aanvraag.VehicleId == null)
            {
                return RedirectToPage();
            }

            Vehicle? vehicle = vehicleService.GetVehicleById(aanvraag.VehicleId.Value);

            if (vehicle == null)
            {
                return RedirectToPage();
            }

            int newMileage = vehicle.Mileage + Convert.ToInt32(aanvraag.AantalKilometers);

            vehicleService.UpdateMileage(vehicle.Id, newMileage);

            return RedirectToPage();
        }
    }
}