using Application.AcademicYears;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AcademicYears;

[ApiController]
[Route("api/academic-years")]
[Authorize]
public class AcademicYearsController(IAcademicYearService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await service.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var result = await service.GetCurrentAsync();
        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAcademicYearDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAcademicYearDto dto)
    {
        var result = await service.UpdateAsync(id, dto);
        return Ok(result.Data);
    }

    [HttpPut("{id:int}/set-current")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetCurrent(int id)
    {
        await service.SetCurrentAsync(id);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await service.DeactivateAsync(id);
        return NoContent();
    }
}
