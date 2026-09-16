namespace TaskManager.Api.DTOs.TaskDTOs;

using TaskManager.Api.Enums;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    
    public string ProjectName { get; set; } = string.Empty;
    public string AssignedUserName { get; set; } = string.Empty;
}