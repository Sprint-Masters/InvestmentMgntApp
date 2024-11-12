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
    public class AdminsController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<AdminsController> _logger;

        public AdminsController(InvestmentDBContext context, ILogger<AdminsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            _logger.LogInformation("Received a Admin Get request");
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            _logger.LogInformation($"Received a Admin Get request by id: {id}");
            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, AdminDTO adminDTO)
        {
            Admin admin=new Admin();
            admin.AdminId=adminDTO.AdminId;
            admin.FirstName=adminDTO.FirstName;
            admin.LastName=adminDTO.LastName;
            admin.AdminEmail=adminDTO.AdminEmail;
            admin.AdminPassword=adminDTO.AdminPassword;

            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Admin Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(AdminDTO adminDTO)
        {
            Admin admin = new Admin();
            admin.AdminId = adminDTO.AdminId;
            admin.FirstName = adminDTO.FirstName;
            admin.LastName = adminDTO.LastName;
            admin.AdminEmail = adminDTO.AdminEmail;
            admin.AdminPassword = adminDTO.AdminPassword;

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated a Admin Post request by id: {admin.AdminId}");
            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted a Admin data belongs to id: {id}");

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
