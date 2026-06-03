using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PaulAlarba.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace PaulAlarba.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string password)
        {
            var correctPassword = _configuration["AdminPassword"];

            if (password == correctPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Messages");
            }

            ViewBag.Error = "Invalid Password";
            return View();
        }

        // GET: /Admin/Messages
        [HttpGet]
        public async Task<IActionResult> Messages()
        {
            // Basic Security Check
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAtUtc)
                .ToListAsync();

            return View(messages);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }
    }
}
