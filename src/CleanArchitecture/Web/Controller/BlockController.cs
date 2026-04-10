using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Shared.Models.Requests.Yards.Blocks;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controller;

[ApiController]
[Route("api/yards/blocks")]
public class BlockController : ControllerBase
{
    private readonly IBlockService _blockService;

    public BlockController(IBlockService blockService)
    {
        _blockService = blockService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _blockService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _blockService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBlockRequest request)
    {
        var result = await _blockService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlockRequest request)
    {
        var result = await _blockService.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _blockService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return Ok(new { message = "Block deleted successfully." });
    }
}
