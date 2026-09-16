using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs;
using TaskManager.Api.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetProjects()
        => Ok(await projectService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
        => Ok(await projectService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> CreateProject(ProjectCreateDto dto)
    {
        var created = await projectService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetProject), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch.");
        await projectService.UpdateAsync(id, dto); 
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await projectService.DeleteAsync(id);
        return NoContent();
    }
}