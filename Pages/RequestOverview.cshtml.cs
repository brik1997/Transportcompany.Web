using Microsoft.AspNetCore.Mvc.RazorPages;
using TransportCompany.Data;

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
            Aanvragen = aanvraagRepository.GetAllAanvragen();
        }
    }
}