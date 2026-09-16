namespace TaskManager.Api.DTOs.TaskDTOs;

using System.ComponentModel.DataAnnotations;
using TaskManager.Api.Enums;

public class TaskCreateDto
{
    [Required(ErrorMessage = "Task title is required")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int ProjectId { get; set; }

    [Required]
    public int AssignedToUserId { get; set; }
}