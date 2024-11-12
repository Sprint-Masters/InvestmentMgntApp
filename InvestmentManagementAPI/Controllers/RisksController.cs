using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestmentManagementAPI.Models;
using System.Composition;
using InvestmentManagementAPI.DTO;

namespace InvestmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<RisksController> _logger;

        public RisksController(InvestmentDBContext context, ILogger<RisksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// To Get the details of the Risks
        /// </summary>
        /// <returns>Display the details of the Risks</returns>
        // GET: api/Risks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Risk>>> GetRisk()
        {
            _logger.LogInformation("Received a Risk Get request");
            return await _context.Risks.ToListAsync();
        }

        /// <summary>
        /// To Get the details of the Risks by id
        /// </summary>
        /// <returns>Display the details of all the Risks</returns>
        // GET: api/Risks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Risk>> GetRisk(int id)
        {
            _logger.LogInformation($"Received a Risk Get request by id: {id}");
            var risk = await _context.Risks.FindAsync(id);

            if (risk == null)
            {
                return NotFound();
            }

            return risk;
        }

        /// <summary>
        /// To Edit the details of Risks by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="riskDTO"></param>
        /// <returns>Displays the edited Risks</returns>
        // PUT: api/Risks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRisk(int id, RiskDTO riskDTO)
        {
            Risk risk=new Risk();
            risk.RiskId=riskDTO.RiskId;
            risk.PortfolioId=riskDTO.PortfolioId;
            risk.RiskLevel=riskDTO.RiskLevel;
            risk.RiskType=riskDTO.RiskType;

            if (id != risk.RiskId)
            {
                return BadRequest();
            }

            _context.Entry(risk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Risk Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskExists(id))
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
        /// To add new Risks
        /// </summary>
        /// <param name="riskDTO"></param>
        /// <returns>Displays all the Risks available</returns>
        // POST: api/Risks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Risk>> PostRisk(RiskDTO riskDTO)
        {
            Risk risk = new Risk();
            risk.RiskId = riskDTO.RiskId;
            risk.PortfolioId = riskDTO.PortfolioId;
            risk.RiskLevel = riskDTO.RiskLevel;
            risk.RiskType = riskDTO.RiskType;

            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Updated a Risk Post request by id: {risk.RiskId}");

            return CreatedAtAction("GetRisk", new { id = risk.RiskId }, risk);
        }

        /// <summary>
        /// To delete the risk by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Displays the remaining risks available</returns>
        // DELETE: api/Risks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRisk(int id)
        {
            var risk = await _context.Risks.FindAsync(id);
            if (risk == null)
            {
                return NotFound();
            }

            _context.Risks.Remove(risk);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted a Risk data belongs to id: {id}");

            return NoContent();
        }

        private bool RiskExists(int id)
        {
            return _context.Risks.Any(e => e.RiskId == id);
        }
    }
}
