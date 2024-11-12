﻿using System;
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
    public class AssetsController : ControllerBase
    {
        private readonly InvestmentDBContext _context;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(InvestmentDBContext context, ILogger<AssetsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAsset()
        {
            _logger.LogInformation("Received a Asset Get request");
            return await _context.Assets.ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            _logger.LogInformation($"Received a Asset Get request by id: {id}");
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // PUT: api/Assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, AssetDTO assetDTO)
        {
            Asset asset=new Asset();
            asset.AssetId = assetDTO.AssetId;
            asset.PortfolioId = assetDTO.PortfolioId;
            asset.AssetType = assetDTO.AssetType;
            asset.CurrentPrice = assetDTO.CurrentPrice;
            asset.Quantity=assetDTO.Quantity;
            asset.Value = assetDTO.Value;
            asset.Symbol= assetDTO.Symbol;
            
            if (id != asset.AssetId)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                _logger.LogInformation($"Updated a Asset Put request by id: {id}");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // POST: api/Assets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(AssetDTO assetDTO)
        {
            Asset asset = new Asset();
            asset.AssetId = assetDTO.AssetId;
            asset.PortfolioId = assetDTO.PortfolioId;
            asset.AssetType = assetDTO.AssetType;
            asset.CurrentPrice = assetDTO.CurrentPrice;
            asset.Quantity = assetDTO.Quantity;
            asset.Value = assetDTO.Value;
            asset.Symbol = assetDTO.Symbol;

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated a Asset Post request by id: {asset.AssetId}");

            return CreatedAtAction("GetAsset", new { id = asset.AssetId }, asset);
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted a Asset data belongs to id: {id}");

            return NoContent();
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetId == id);
        }
    }
}