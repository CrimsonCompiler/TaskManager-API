using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using Xunit; 

namespace TaskManager.Api.Tests;

public class ProjectServiceTests
{
   
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProject_WhenProjectExists()
    {
    
        var context = CreateInMemoryContext();
        
        context.Projects.Add(new Project 
        { 
            Id = 1, 
            Name = "Test Project", 
            Description = "Test Desc", 
            CreatedAt = DateTime.UtcNow 
        });
        await context.SaveChangesAsync();

        var service = new ProjectService(context);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Project", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenProjectDoesNotExist()
    {
        var context = CreateInMemoryContext();
        var service = new ProjectService(context);
        
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(999));
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedProject()
    {
        var context = CreateInMemoryContext();
        var service = new ProjectService(context);
        
        var dto = new ProjectCreateDto 
        { 
            Name = "New Project", 
            Description = "New Desc" 
        };
        
        var result = await service.CreateAsync(dto);
        
        Assert.NotNull(result);
        Assert.Equal("New Project", result.Name);
        Assert.Equal(0, result.TotalTasks);
    }
}