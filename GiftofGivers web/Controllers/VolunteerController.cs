using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GiftofGivers_web.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly AppDbContext _context;
        public VolunteerController(AppDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Register()
        {
            var volunteers = _context.Volunteers.ToList();
            return View(volunteers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                if (volunteer.Id == 0)
                    _context.Volunteers.Add(volunteer);
                else
                    _context.Volunteers.Update(volunteer);

                _context.SaveChanges();
                return RedirectToAction(nameof(Register));
            }

            var volunteers = _context.Volunteers.ToList();
            return View(volunteers);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var volunteer = _context.Volunteers.Find(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Register));
        }
    }
}
