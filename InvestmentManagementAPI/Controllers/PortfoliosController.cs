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
    public class PortfoliosController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<PortfoliosController> _logger; 

        public PortfoliosController(InvestmentDBContext context,ILogger<PortfoliosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Portfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolio()
        {
            _logger.LogInformation("Received a Portfolio Get request");
            return await _context.Portfolios.ToListAsync();
        }

        // GET: api/Portfolios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            _logger.LogInformation($"Received a Portfolio Get request by id: {id}");
            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio(int id, PortfolioDTO portfolioDTO)
        {
            Portfolio portfolio=new Portfolio();
            portfolio.PortfolioId = portfolioDTO.PortfolioId;
            portfolio.Name = portfolioDTO.Name;
            portfolio.UserId=portfolioDTO.UserId;
            portfolio.Description= portfolioDTO.Description;
            portfolio.TotalValue = portfolioDTO.TotalValue;

            if (id != portfolio.PortfolioId)
            {
                return BadRequest();
            }

            _context.Entry(portfolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Portfolio Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioExists(id))
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

        // POST: api/Portfolios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Portfolio>> PostPortfolio(PortfolioDTO portfolioDTO)
        {
            Portfolio portfolio = new Portfolio();
            portfolio.PortfolioId = portfolioDTO.PortfolioId;
            portfolio.Name = portfolioDTO.Name;
            portfolio.UserId = portfolioDTO.UserId;
            portfolio.Description = portfolioDTO.Description;
            portfolio.TotalValue = portfolioDTO.TotalValue;

            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated a Portfolio Post request by id: {portfolio.PortfolioId}");

            return CreatedAtAction("GetPortfolio", new { id = portfolio.PortfolioId }, portfolio);
        }

        // DELETE: api/Portfolios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted a Portfolio data belongs to id: {id}");

            return NoContent();
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.PortfolioId == id);
        }
    }
}
