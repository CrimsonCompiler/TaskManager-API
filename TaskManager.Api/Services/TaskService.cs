using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs.TaskDTOs;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Interfaces;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public class TaskService(AppDbContext context) : ITaskService
{
    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
    {
        return await context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedUser)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                ProjectName = t.Project.Name,
                AssignedUserName = t.AssignedUser.Name
            })
            .ToListAsync();
    }

    public async Task<TaskResponseDto> GetByIdAsync(int id) // আর nullable (?) দরকার নেই
    {
        var taskItem = await context.TaskItems.FindAsync(id);
        if (taskItem == null) 
            throw new NotFoundException($"Task with ID {id} not found.");
        
        var taskWithRelations = await context.TaskItems?
            .Include(t => t.Project)
            .Include(t => t.AssignedUser)
            .FirstOrDefaultAsync(t => t.Id == id);

        return new TaskResponseDto
        {
            Id = taskWithRelations.Id,
            Title = taskWithRelations.Title,
            Description = taskWithRelations.Description,
            Status = taskWithRelations.Status,
            Priority = taskWithRelations.Priority,
            DueDate = taskWithRelations.DueDate,
            ProjectName = taskWithRelations.Project.Name,
            AssignedUserName = taskWithRelations.AssignedUser.Name
        };
    }

    public async Task<TaskResponseDto> CreateAsync(TaskCreateDto dto)
    {
        var taskItem = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId,
            AssignedToUserId = dto.AssignedToUserId
        };

        context.TaskItems.Add(taskItem);
        await context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status,
            Priority = taskItem.Priority,
            DueDate = taskItem.DueDate,
            ProjectName = "",
            AssignedUserName = ""
        };
    }

    public async Task<bool> UpdateAsync(int id, TaskUpdateDto dto)
    {
        var taskItem = await context.TaskItems.FindAsync(id);
        if (taskItem == null) 
            throw new NotFoundException($"Task with ID {id} not found.");

        taskItem.Title = dto.Title;
        taskItem.Description = dto.Description;
        taskItem.Status = dto.Status;
        taskItem.Priority = dto.Priority;
        taskItem.DueDate = dto.DueDate;
        taskItem.ProjectId = dto.ProjectId;
        taskItem.AssignedToUserId = dto.AssignedToUserId;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var taskItem = await context.TaskItems.FindAsync(id);
        if (taskItem == null) 
            throw new NotFoundException($"Task with ID {id} not found.");

        context.TaskItems.Remove(taskItem);
        await context.SaveChangesAsync();
        return true;
    }
}