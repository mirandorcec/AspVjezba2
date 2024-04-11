using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index(string? query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var clients = MockClientRepository.Instance.All().Where(c => c.FullName.ToLower().Contains(query.ToLower())).ToList();
                return View(clients);
            }
            else
            {
                var clients = MockClientRepository.Instance.All().ToList();
                return View(clients);
            }
        }

        [HttpPost]
        public IActionResult Index(string queryName, string queryAddress)
        {
            var clients = MockClientRepository.Instance.All().ToList();
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                clients = clients.Where(c => c.FullName.ToLower().Contains(queryName.ToLower())).ToList();

            }
            if (!string.IsNullOrWhiteSpace(queryAddress))
            {
                clients = clients.Where(c => c.Address.ToLower().Contains(queryAddress.ToLower())).ToList();
            }

            return View(clients);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMessage = "ID nije proslijeđen.";
                return View("Error");
            }
            var client = MockClientRepository.Instance.FindByID(id.Value);
            if (client == null)
            {
                ViewBag.ErrorMessage = $"Klijent s ID-om {id.Value} nije pronađen.";
                return View("Error");
            }
            return View(client);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult AdvancedSearch(ClientFilterModel model)
        {
            var clients = MockClientRepository.Instance.All().ToList();
            if (!string.IsNullOrWhiteSpace(model.FullName))
            {
                clients = clients.Where(c => c.FullName.ToLower().Contains(model.FullName.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                clients = clients.Where(c => c.Email.ToLower().Contains(model.Email.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(model.Address))
            {
                clients = clients.Where(c => c.Address.ToLower().Contains(model.Address.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(model.City))
            {
                clients = clients.Where(c => c.City != null && c.City.Name.ToLower().Contains(model.City.ToLower())).ToList();
            }
            return View("Index", clients);
        }

    }
}
