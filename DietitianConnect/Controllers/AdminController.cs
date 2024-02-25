using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _sqlConnection;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public AdminController(DietitianContext dietitianContext, IConfiguration configuration)
        {
            _dietitianContext = dietitianContext;
            _configuration = configuration;

            // IConfiguration aracılığıyla bağlantı dizesini al
            string connectionString = _configuration.GetConnectionString("dbcon");
            _sqlConnection = new SqlConnection(connectionString);

        }

        [HttpPost]
        [Route("Login")]
        public string Login(Admin admin)
        {
            string msg = string.Empty;
            try
            {
                // Giriş işlemleri
                da = new SqlDataAdapter("usp_adm_Login", _sqlConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", admin.Email);
                da.SelectCommand.Parameters.AddWithValue("@PasswordHash", admin.PasswordHash);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Giriş başarılı mı kontrolü
                if (dt.Rows.Count > 0)
                {
                    msg = "Admin is valid";
                }
                else
                {
                    msg = "Admin is Invalid";
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
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