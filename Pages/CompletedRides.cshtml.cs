using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using TransportCompany.Data;
using TransportCompany.Logic;

namespace TransportCompany.Web.Pages
{
    public class CompletedRidesModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<Aanvraag> Aanvragen { get; set; } = new();

        public CompletedRidesModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            Aanvragen = aanvraagService.GetAllAanvragen()
                .Where(a => a.Status == "Completed")
                .ToList();
        }
    }
}