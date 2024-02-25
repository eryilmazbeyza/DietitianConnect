using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _sqlConnection;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public UserController(DietitianContext dietitianContext, IConfiguration configuration)
        {
            _dietitianContext = dietitianContext;
            _configuration = configuration;

            // IConfiguration aracılığıyla bağlantı dizesini al
            string connectionString = _configuration.GetConnectionString("dbcon");
            _sqlConnection = new SqlConnection(connectionString);
            
        }

        [HttpPost]
        [Route("Registration")]
        public string Registration(User user)
        {
            string msg = string.Empty;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Registration", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Diğer SqlCommand ayarlarını buraya ekleyebilirsiniz.
                    // Örnek: cmd.Parameters.AddWithValue("@ParameterName", parameterValue);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@PasswordHarsh", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    _sqlConnection.Open();
                    int i = cmd.ExecuteNonQuery();
                    _sqlConnection.Close();
                    if (i > 0)
                    {
                        msg = "Data inserted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
                return msg;
        }

        [HttpPost]
        [Route("User/Login")]
        public string UserLogin(User user)
        {
            string msg = string.Empty;
            try
            {
               da = new SqlDataAdapter("usp_Login", _sqlConnection);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", user.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", user.PasswordHash);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    msg = "User is valid";
                }
                else 
                { 
                    msg = "User is Invalid"; 
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
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
