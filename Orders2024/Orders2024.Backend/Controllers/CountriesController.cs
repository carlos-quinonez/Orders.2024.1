using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders2024.Backend.Data;
using Orders2024.Shared.Entities;
using System.Linq;

namespace Orders2024.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Countries.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        if (string.IsNullOrEmpty(country.Id.ToString())) { country.Id = Guid.NewGuid(); }
        _context.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(Country country)
    {
        _context.Update(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        _context.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}