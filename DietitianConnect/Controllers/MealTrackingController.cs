using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTrackingController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public MealTrackingController(DietitianContext dietitianContext)
        {
            _dietitianContext = dietitianContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealTracking>>> GetMealTrackings()
        {
            if (_dietitianContext.MealTrackings == null)
            {
                return NotFound();
            }
            return await _dietitianContext.MealTrackings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealTracking>> GetMealTrackings(int id)
        {
            if (_dietitianContext.MealTrackings == null)
            {
                return NotFound();
            }
            var mealtracking = await _dietitianContext.MealTrackings.FindAsync(id);
            if (mealtracking == null)
            {
                return NotFound();
            }

            return mealtracking;
        }

        [HttpPost]
        public async Task<ActionResult<MealTracking>> PostMealTracking(MealTracking mealtracking)
        {
            _dietitianContext.MealTrackings.Add(mealtracking);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMealTrackings), new { id = mealtracking.TrackingID }, mealtracking);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMealTracking(int id, MealTracking tracking)
        {
            if (id != tracking.TrackingID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(tracking).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteMealTracking(int id)
        {
            if (_dietitianContext.MealTrackings== null)
            {
                return NotFound();
            }
            var tracking = await _dietitianContext.MealTrackings.FindAsync(id);
            if (tracking == null)
            {
                return NotFound();
            }
            _dietitianContext.MealTrackings.Remove(tracking);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
