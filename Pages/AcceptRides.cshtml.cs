using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using TransportCompany.Data;
using TransportCompany.Logic;

namespace TransportCompany.Web.Pages
{
    public class AcceptRidesModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<Aanvraag> Aanvragen { get; set; } = new();

        public AcceptRidesModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            Aanvragen = aanvraagService.GetAllAanvragen()
                .Where(a => a.Status == "Accepted")
                .ToList();
        }

        public IActionResult OnPostComplete(int id)
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            aanvraagService.CompleteAanvraag(id);

            return RedirectToPage();
        }
    }
}