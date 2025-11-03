using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GiftofGivers_web.Controllers
{
    public class IncidentController : Controller
    {
        private readonly AppDbContext _context;
        public IncidentController(AppDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Create()
        {
            var incidents = _context.Incidents.ToList();
            return View(incidents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.Id == 0)
                    _context.Incidents.Add(incident);
                else
                    _context.Incidents.Update(incident);

                _context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }

            var incidents = _context.Incidents.ToList();
            return View(incidents);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Create));
        }
    }
}
