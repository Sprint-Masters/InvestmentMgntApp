using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentManagementAPI.Models;
using System.Data;
using InvestmentManagementAPI.DTO;

namespace InvestmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<RolesController> _logger;

        public RolesController(InvestmentDBContext context, ILogger<RolesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// To Get the Details of the Roles
        /// </summary>
        /// <returns>Displays all the roles</returns>
        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            _logger.LogInformation($"Received a Role Get request");
            return await _context.Roles.ToListAsync();
        }

        /// <summary>
        /// To Get thye details of roles by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Displays the details of the Roles</returns>
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            _logger.LogInformation($"Received a Role Get request by id: {id}");
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        /// <summary>
        /// To Edit the Roles by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleDTO"></param>
        /// <returns>Displays the edited details of Roles</returns>
        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDTO roleDTO)
        {
            Role role = new Role();
            role.RoleId = roleDTO.RoleId;
            role.RoleName = roleDTO.RoleName;

            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Role Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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
        /// To Add new Roles to the database
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns>Displays the details of the Roles</returns>
        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDTO roleDTO)
        {
            Role role = new Role();
            role.RoleId = roleDTO.RoleId;
            role.RoleName = roleDTO.RoleName;

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated a Role Post request by id: {role.RoleId}");
            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        /// <summary>
        /// To delete the Roles by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Displays the remaining details of roles</returns>
        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted a Role data belongs to id: {id}");

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
