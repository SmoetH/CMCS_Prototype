using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CMCS_Prototype.Models;
using System.Linq;

namespace CMCS_Prototype.Controllers
{
    public class AccountController : Controller
    {
        private readonly CmcContext _context;

        public AccountController(CmcContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Set both the IsAuthenticated status and the Username in the session.
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginError = "Invalid username or password. Please try again.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.RegisterError = "Username already exists. Please choose a different one.";
                return View();
            }

            var newUser = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Automatically log in the new user after successful registration.
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("Username", newUser.Username);
            return RedirectToAction("Index", "Home");
        }
    }
}
