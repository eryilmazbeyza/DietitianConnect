using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public UserController(DietitianContext dietitianContext)
        {
            _dietitianContext = dietitianContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_dietitianContext.Users == null)
            {
                return NotFound();
            }
            return await _dietitianContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            if (_dietitianContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dietitianContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _dietitianContext.Users.Add(user);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(user).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_dietitianContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dietitianContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dietitianContext.Users.Remove(user);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
