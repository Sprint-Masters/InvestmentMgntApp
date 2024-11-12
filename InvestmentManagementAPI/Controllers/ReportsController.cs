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
    public class ReportsController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(InvestmentDBContext context, ILogger<ReportsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport()
        {
            _logger.LogInformation("Received a Report Get request");
            return await _context.Reports.ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            _logger.LogInformation($"Received a Report Get request by id:{id}");
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, ReportDTO reportDTO)
        {
            Report report=new Report();
            report.ReportId=reportDTO.ReportId;
            report.PortfolioId=reportDTO.PortfolioId;
            report.ReportType=reportDTO.ReportType;
            report.ReportDate=reportDTO.ReportDate;

            if (id != report.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated a Report Put request by id: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(ReportDTO reportDTO)
        {
            Report report = new Report();
            report.ReportId = reportDTO.ReportId;
            report.PortfolioId = reportDTO.PortfolioId;
            report.ReportType = reportDTO.ReportType;
            report.ReportDate = reportDTO.ReportDate;

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Updated a Report Post request by id: {report.ReportId}");

            return CreatedAtAction("GetReport", new { id = report.ReportId }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted a Report data belongs to id: {id}");

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
