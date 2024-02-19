using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public DietPlanController(DietitianContext dietitianContext)
        {
            _dietitianContext = dietitianContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietPlan>>> GetDietPlans()
        {
            if (_dietitianContext.DietPlans == null)
            {
                return NotFound();
            }
            return await _dietitianContext.DietPlans.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DietPlan>> GetDietPlans(int id)
        {
            if (_dietitianContext.DietPlans == null)
            {
                return NotFound();
            }
            var dietplan = await _dietitianContext.DietPlans.FindAsync(id);
            if (dietplan == null)
            {
                return NotFound();
            }

            return dietplan;
        }

        [HttpPost]
        public async Task<ActionResult<DietPlan>> PostDietPlan(DietPlan dietPlan)
        {
            _dietitianContext.DietPlans.Add(dietPlan);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDietPlans), new { id = dietPlan.PlanID }, dietPlan);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDietPlan(int id, DietPlan dietPlan)
        {
            if (id != dietPlan.PlanID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(dietPlan).State = EntityState.Modified;
            try
            {
                await _dietitianContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDietPlan(int id)
        {
            if (_dietitianContext.DietPlans == null)
            {
                return NotFound();
            }
            var dietPlan = await _dietitianContext.DietPlans.FindAsync(id);
            if (dietPlan == null)
            {
                return NotFound();
            }
            _dietitianContext.DietPlans.Remove(dietPlan);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
