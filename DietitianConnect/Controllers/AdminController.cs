using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public AdminController(DietitianContext dietitianContext)
        {
            _dietitianContext = dietitianContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            if (_dietitianContext.Admins == null)
            {
                return NotFound();
            }
            return await _dietitianContext.Admins.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmins(int id)
        {
            if (_dietitianContext.Admins == null)
            {
                return NotFound();
            }
            var admin = await _dietitianContext.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _dietitianContext.Admins.Add(admin);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdmins), new { id = admin.AdminID }, admin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(admin).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            if (_dietitianContext.Admins == null)
            {
                return NotFound();
            }
            var admin = await _dietitianContext.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            _dietitianContext.Admins.Remove(admin);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
