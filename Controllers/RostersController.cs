using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBowlMVC.Data;
using BloodBowlMVC.Models;

namespace BloodBowlMVC.Controllers
{
    public class RostersController : Controller
    {
        private readonly BloodBowlDbContext _context;

        public RostersController(BloodBowlDbContext context)
        {
            _context = context;
        }

        // GET: Rosters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rosters.ToListAsync());
        }

        // GET: Rosters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .FirstOrDefaultAsync(m => m.RosterIdPk == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // GET: Rosters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rosters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RosterIdPk,RosterRace,RosterPosition,RosterCost,RosterMovement,RosterStrength,RosterAgility,RosterArmor")] Roster roster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roster);
        }

        // GET: Rosters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters.FindAsync(id);
            if (roster == null)
            {
                return NotFound();
            }
            return View(roster);
        }

        // POST: Rosters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RosterIdPk,RosterRace,RosterPosition,RosterCost,RosterMovement,RosterStrength,RosterAgility,RosterArmor")] Roster roster)
        {
            if (id != roster.RosterIdPk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterExists(roster.RosterIdPk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roster);
        }

        // GET: Rosters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .FirstOrDefaultAsync(m => m.RosterIdPk == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // POST: Rosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);
            if (roster != null)
            {
                _context.Rosters.Remove(roster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterExists(int id)
        {
            return _context.Rosters.Any(e => e.RosterIdPk == id);
        }
    }
}
