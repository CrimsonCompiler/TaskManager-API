using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Exceptions; // এটি যোগ করুন
using TaskManager.Api.Interfaces;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public class ProjectService(AppDbContext context) : IProjectService
{
    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        return await context.Projects
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                TotalTasks = p.Tasks.Count
            })
            .ToListAsync();
    }

    public async Task<ProjectResponseDto> GetByIdAsync(int id)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null) 
            throw new NotFoundException($"Project with ID {id} not found.");

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            TotalTasks = project.Tasks.Count
        };
    }

    public async Task<ProjectResponseDto> CreateAsync(ProjectCreateDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        context.Projects.Add(project);
        await context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            TotalTasks = 0
        };
    }

    public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
    {
        var existingProject = await context.Projects.FindAsync(id);
        if (existingProject == null) 
            throw new NotFoundException($"Project with ID {id} not found.");

        existingProject.Name = dto.Name;
        existingProject.Description = dto.Description;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingProject = await context.Projects.FindAsync(id);
        if (existingProject == null) 
            throw new NotFoundException($"Project with ID {id} not found.");

        context.Projects.Remove(existingProject);
        await context.SaveChangesAsync();
        return true;
    }
}