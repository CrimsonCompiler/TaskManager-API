using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs.UserDTOs;
using TaskManager.Api.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
        return Ok(await userService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponseDto>> GetUser(int id)
    {
        return Ok(await userService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUser(UserCreateDto dto)
    {
        var createdUser = await userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        await userService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteAsync(id);
        return NoContent();
    }
}