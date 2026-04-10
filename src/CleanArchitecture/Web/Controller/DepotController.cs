using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Shared.Models.Requests.Yards.Depots;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;

[ApiController]
[Route("api/yards/depots")]
public class DepotController : ControllerBase
{
    private readonly IDepotService _depotService;

    public DepotController(IDepotService depotService)
    {
        _depotService = depotService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _depotService.GetAllAsync();
        return Ok(new { items = result, totalCount = result.Count });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _depotService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(new { data = result });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepotRequest request)
    {
        var result = await _depotService.CreateAsync(request);
        return Ok(new { data = result });
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepotRequest request)
    {
        var result = await _depotService.UpdateAsync(id, request);
        if (result == null) return NotFound();

        return Ok(new { data = result });
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _depotService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return Ok(new { message = "Depot deleted successfully." });
    }
}
