using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GiftofGivers_web.Controllers
{
    public class DonationController : Controller
    {
        private readonly AppDbContext _context;
        public DonationController(AppDbContext context) => _context = context;

        // Display form and list
        [HttpGet]
        public IActionResult Create()
        {
            var donations = _context.Donations.ToList();
            return View(donations);
        }

        // Add or Edit donation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                if (donation.Id == 0)
                    _context.Donations.Add(donation);
                else
                    _context.Donations.Update(donation);

                _context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }

            var donations = _context.Donations.ToList();
            return View(donations);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var donation = _context.Donations.Find(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Create));
        }
    }
}
