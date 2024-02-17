using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietitianController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public DietitianController(DietitianContext dietitianContext)
        {
            _dietitianContext= dietitianContext;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dietitian>>> GetDietitians()
        {
            if (_dietitianContext.Dietitians == null)
            {
                return NotFound();
            }
            return await _dietitianContext.Dietitians.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dietitian>> GetDietitians(int id)
        {
            if (_dietitianContext.Dietitians == null)
            {
                return NotFound();
            }
            var dietitian =await _dietitianContext.Dietitians.FindAsync(id);
            if(dietitian == null)
            {
                return NotFound();
            }
            
            return dietitian;
        }

        [HttpPost]
        public async Task<ActionResult<Dietitian>> PostDietitian(Dietitian dietitian)
        {
            _dietitianContext.Dietitians.Add(dietitian);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDietitians), new {id=dietitian.DietitianID},dietitian);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDietitian(int id, Dietitian dietitian)
        {
            if (id != dietitian.DietitianID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(dietitian).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteDietitian(int id)
        {
            if (_dietitianContext.Dietitians == null)
            {
                return NotFound();
            }
            var dietitian = await _dietitianContext.Dietitians.FindAsync(id);
            if (dietitian == null)
            {
                return NotFound();
            }
            _dietitianContext.Dietitians.Remove(dietitian);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
