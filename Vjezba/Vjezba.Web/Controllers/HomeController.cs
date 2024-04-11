using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> _logger)
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";

            return View();
        }

        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            var ime = formData["ime"];
            var prezime = formData["prezime"];
            var email = formData["email"];
            var poruka = formData["poruka"];
            var tipPoruke = formData["tipPoruke"];
            var newsletter = formData["newsletter"].Count > 0 ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";

            var responsePoruka = $"Poštovani {ime} {prezime} ({email}) zaprimili smo vašu poruku te će vam se netko ubrzo javiti. Sadržaj vaše poruke je: {tipPoruke} - {poruka}. Također, {newsletter} o daljnjim promjenama preko newslettera.";

            ViewBag.ResponsePoruka = responsePoruka;

            return View("ContactSuccess");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FAQ(int? selected = null)
        {
            ViewBag.SelectedQuestion = selected;
            
            var faqs = new List<Tuple<string, string>>()
            {
                new("Tko bude prvak HNL-a?", "Dinamo"),
                new("Tko bude prvak premier lige", "Liverpool"),
                new("Tko će osvojiti ligu prvaka?", "Manchester City :("),
                new("Tko je najbolji igrač svih vremena?", "The little boy from Rosario"),
                new("Tko osvaja bundesligu?", "Leverkuseeeen")
             };

            ViewBag.FAQs = faqs;

            return View();
        }

        public IActionResult ContactSuccess()
        {
            return View();
        }
    }
}