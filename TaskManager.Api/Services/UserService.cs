using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs.UserDTOs;
using TaskManager.Api.Exceptions; 
using TaskManager.Api.Interfaces;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        return await context.Users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role
        }).ToListAsync();
    }

    public async Task<UserResponseDto> GetByIdAsync(int id) // আর nullable (?) দরকার নেই
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) 
            throw new NotFoundException($"User with ID {id} not found.");

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password),
            PasswordSalt = GenerateSalt(),
            Role = dto.Role
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) 
            throw new NotFoundException($"User with ID {id} not found.");

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Role = dto.Role;

        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.PasswordHash = HashPassword(dto.Password);
            user.PasswordSalt = GenerateSalt();
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) 
            throw new NotFoundException($"User with ID {id} not found.");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static string GenerateSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }
}