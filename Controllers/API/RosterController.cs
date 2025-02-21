using BloodBowlMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace BloodBowlMVC.Controllers.API
{
    [Route("api/roster")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private readonly BloodBowlDbContext _context;

        public RosterController(BloodBowlDbContext context)
        {
            _context = context;
        }

        [HttpGet("{rosterRace}")]
        public async Task<IActionResult> GetRosterByRace(string rosterRace)
        {
            var roster = await _context.Rosters
                .Where(r => r.RosterRace == rosterRace)
                .Include(r => r.RosterSkillsSkillsIdFks)  // Include the related skills via the many-to-many mapping
                .ToListAsync();

            if (roster == null || !roster.Any())
            {
                return NotFound();
            }

            var result = roster.Select(r => new
            {
                r.RosterIdPk,
                r.RosterRace,
                r.RosterPosition,
                r.RosterCost,
                r.RosterMovement,
                r.RosterStrength,
                r.RosterAgility,
                r.RosterArmor,
                Skills = r.RosterSkillsSkillsIdFks.Select(s => s.SkillName).ToList()
            });

            return Ok(result);
        }

          
    }
}