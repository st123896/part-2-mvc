using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W98.Models;

namespace W98.Controllers
{
    public class ProgrammeCoordinator : Controller
    {
        private readonly LecturerClaimDbContext _context;

        public ProgrammeCoordinator(LecturerClaimDbContext context)
        {
            _context = context;
        }

        // GET: ProgrammeManager/Claims
        public IActionResult Index()
        {
            //var claims = _context.LecturerClaims.ToList();
            return View();
        }

        // GET: ProgrammeManager/Review/5
        public IActionResult Review(int id)
        {
            var claim = _context.LecturerClaims.Find(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: ProgrammeManager/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id, string feedback)
        {
            var claim = _context.LecturerClaims.Find(id);
            if (claim != null)
            {
                claim.IsApproved = true;
                claim.Feedback = feedback;
                _context.SaveChanges();
            }
            return RedirectToAction("Details");
        }

        // POST: ProgrammeManager/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id, string feedback)
        {
            var claim = _context.LecturerClaims.Find(id);
            if (claim != null)
            {
                claim.IsApproved = false;
                claim.Feedback = feedback;
                _context.SaveChanges();
            }
            return RedirectToAction("Details");

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
                return RedirectToAction("Login", "ProgrammeCoordinator");
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
                    return RedirectToAction("Details", "ProgrammeCoordinator");
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
