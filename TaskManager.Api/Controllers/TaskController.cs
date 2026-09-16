using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs.TaskDTOs;
using TaskManager.Api.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
    {
        return Ok(await taskService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
    {
        return Ok(await taskService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> CreateTask(TaskCreateDto dto)
    {
        var createdTask = await taskService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        await taskService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await taskService.DeleteAsync(id);
        return NoContent();
    }
}