using Microsoft.AspNetCore.Mvc;
using CMCS_Prototype.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CMCS_Prototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly CmcContext _context;

        public HomeController(CmcContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SubmitClaim()
        {
            // Check if the user is authenticated first.
            if (HttpContext.Session.GetString("IsAuthenticated") == "true")
            {
                // Retrieve the logged-in user's username from the session.
                var loggedInUser = HttpContext.Session.GetString("Username");
                ViewBag.Username = loggedInUser;
                // Get claims for the logged-in user only
                var lecturerClaims = _context.Claims.Where(c => c.LecturerName == loggedInUser).ToList();
                return View(lecturerClaims);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ApproveClaims()
        {
            // First, check if the user is authenticated.
            if (HttpContext.Session.GetString("IsAuthenticated") == "true")
            {
                // Now, check if the logged-in user is an admin.
                var adminUsernames = new[] { "Admin" };
                var loggedInUser = HttpContext.Session.GetString("Username");
                if (adminUsernames.Contains(loggedInUser))
                {
                    var claims = _context.Claims.ToList();
                    return View(claims);
                }
                else
                {
                    // If a non-admin tries to access this page, redirect them.
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult SubmitClaim(string claimantName, string month, int hours)
        {
            var newClaim = new Claim
            {
                LecturerName = claimantName,
                ClaimMonth = month,
                Hours = hours,
                Status = "Pending"
            };

            _context.Claims.Add(newClaim);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claimToUpdate = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claimToUpdate != null)
            {
                claimToUpdate.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("ApproveClaims");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claimToUpdate = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claimToUpdate != null)
            {
                claimToUpdate.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("ApproveClaims");
        }
    }
}
