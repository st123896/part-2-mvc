using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W98.Models;

namespace W98.Controllers
{
    public class LecturerClaim : Controller
    {
        private readonly ILogger<LecturerClaim> _logger;
        private readonly LecturerClaimDbContext _context;

        public LecturerClaim(ILogger<LecturerClaim> _logger, LecturerClaimDbContext context)
        {
            _logger = _logger;
            _context = context;
        }

        // GET: /LecturerClaim/
        public IActionResult Index()
        {
            //var claims = _context.LecturerClaims.ToList();
            return View();
        }

        public IActionResult CreateEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEdit(W98.Models.LecturerClaim model)
        {
            if (ModelState.IsValid)
            {
                _context.LecturerClaims.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(model);
        }

        // GET: LecturerClaim/Delete/5
        public IActionResult Delete(int id)
        {
            var lecturerClaim = _context.LecturerClaims.Find(id);
            if (lecturerClaim == null)
            {
                return NotFound();
            }
            return View(lecturerClaim);
        }

        // POST: LecturerClaim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lecturerClaim = _context.LecturerClaims.Find(id);
            if (lecturerClaim != null)
            {
                _context.LecturerClaims.Remove(lecturerClaim);
                _context.SaveChanges();
            }
            return RedirectToAction("Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Register_user(W98.Models.register model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user instance
                var user = new register
                {
                    username = model.username,
                    email = model.email,
                    role = model.role,
                    password = model.password // Ensure you hash this in a real application
                };

                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to a success page or login
                return RedirectToAction("Login", "LecturerClaim");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login_user(Check_login model)
        {
            // Validate the model
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.email == model.email && u.password == model.password); // Ensure password is hashed in a real application

                if (user != null)
                {
                    // Optionally, you can set up user claims and sign in here

                    // Redirect to the Details view if login is successful
                    return RedirectToAction("Details", "LecturerClaim");
                }
                else
                {
                    // Add an error message if the login fails
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public IActionResult Details()
        {
            // Here you can retrieve the data you need, if necessary
            var claims = _context.LecturerClaims.ToList();

            return View(claims);
        }
    }
}

