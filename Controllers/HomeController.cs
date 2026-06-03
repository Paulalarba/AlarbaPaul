using Microsoft.AspNetCore.Mvc;
using PaulAlarba.Models;
using PaulAlarba.Services;
using System.Diagnostics;

namespace PaulAlarba.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactMessageStore _contactMessageStore;

        public HomeController(ContactMessageStore contactMessageStore)
        {
            _contactMessageStore = contactMessageStore;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeIndexViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(HomeIndexViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                TempData["SkipLoading"] = true;
                return View("Index", model);
            }

            await _contactMessageStore.SaveAsync(model.Contact, cancellationToken);
            TempData["ContactStatus"] = "Your message has been sent. I’ll get back to you soon.";
            TempData["SkipLoading"] = true;

            return Redirect(Url.Action(nameof(Index), "Home") + "#contact");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
