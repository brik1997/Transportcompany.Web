using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TransportCompany.Data;
using TransportCompany.Logic;

namespace TransportCompany.Web.Pages
{
    public class RequestOverviewModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<Aanvraag> Aanvragen { get; set; } = new();

        public RequestOverviewModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagRepository aanvraagRepository = new AanvraagRepository(connectionString);
            Aanvragen = aanvraagRepository.GetAllAanvragen()
                .Where(a => a.Status == "New")
                .ToList();
        }
        public IActionResult OnPostAccept(int id)
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            aanvraagService.acceptAanvraag(id);

            return RedirectToPage();
        }

        public IActionResult OnPostReject(int id)
        {
            string connectionString = _configuration.GetConnectionString("TransportDB");

            AanvraagService aanvraagService = new AanvraagService(connectionString);
            aanvraagService.RejectAanvraag(id);

            return RedirectToPage();
        }

        
    }
}