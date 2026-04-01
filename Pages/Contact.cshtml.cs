using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using TransportCompany.Data;

namespace Transportbedrijf.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string Naam { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Telefoon { get; set; } = string.Empty;

        [BindProperty]
        public string AanvraagType { get; set; } = string.Empty;

        [BindProperty]
        public string OphaalStraat { get; set; } = string.Empty;

        [BindProperty]
        public string OphaalPostcode { get; set; } = string.Empty;

        [BindProperty]
        public string OphaalStad { get; set; } = string.Empty;

        [BindProperty]
        public string AfzetStraat { get; set; } = string.Empty;

        [BindProperty]
        public string AfzetPostcode { get; set; } = string.Empty;

        [BindProperty]
        public string AfzetStad { get; set; } = string.Empty;

        [BindProperty]
        public decimal AantalKilometers { get; set; }

        [BindProperty]
        public string Bericht { get; set; } = string.Empty;

        public string Melding { get; set; } = string.Empty;

        public ContactModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string connectionString = _configuration.GetConnectionString("TransportDb") ?? string.Empty;

            Aanvraag aanvraag = new Aanvraag
            {
                Naam = Naam,
                Email = Email,
                Telefoon = Telefoon,
                AanvraagType = AanvraagType,
                OphaalStraat = OphaalStraat,
                OphaalPostcode = OphaalPostcode,
                OphaalStad = OphaalStad,
                AfzetStraat = AfzetStraat,
                AfzetPostcode = AfzetPostcode,
                AfzetStad = AfzetStad,
                AantalKilometers = AantalKilometers,
                Bericht = Bericht
            };

            AanvraagRepository repository = new AanvraagRepository(connectionString);
            repository.Insert(aanvraag);

            Melding = "Aanvraag is opgeslagen.";
        }
    }
}