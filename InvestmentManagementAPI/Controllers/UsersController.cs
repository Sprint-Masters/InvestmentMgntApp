using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentManagementAPI.Models;
using InvestmentManagementAPI.DTO;

namespace InvestmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(InvestmentDBContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// To Get the Details of the Users
        /// </summary>
        /// <returns>Displays all the Users</returns>
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            _logger.LogInformation("Received a User Get request");
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// To Get thye details of Users by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Displays the details of the Users</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation($"Received a User Get request by id: {id}");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// To Edit the Users by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleDTO"></param>
        /// <returns>Displays the edited details of Users</returns>
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            User user=new User();
            user.UserId=userDTO.UserId;
            user.FirstName=userDTO.FirstName;
            user.LastName=userDTO.LastName;
            user.Email=userDTO.Email;
            user.PasswordHash=userDTO.PasswordHash;
            user.RoleId=userDTO.RoleId;

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a User Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// To Add new Users to the database
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns>Displays the details of the Users</returns>
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
        {
            User user = new User();
            user.UserId = userDTO.UserId;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PasswordHash = userDTO.PasswordHash;
            user.RoleId = userDTO.RoleId;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Received a User Post request by id: {user.UserId}");
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        /// <summary>
        /// To delete the Users by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Displays the remaining details of Users</returns>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted a User data belongs to id: {id}");

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
