using Application.GradingSchemes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GradingSchemes;

[ApiController]
[Route("api/grading-schemes")]
[Authorize(Roles = "Admin,Manager")]
public class GradingSchemesController(IGradingSchemeService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int subjectId, [FromQuery] int semesterId)
    {
        var result = await service.GetBySubjectAndSemesterAsync(subjectId, semesterId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateGradingSchemeDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGradingSchemeDto dto)
    {
        var result = await service.UpdateAsync(id, dto);
        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await service.DeactivateAsync(id);
        return NoContent();
    }
}
