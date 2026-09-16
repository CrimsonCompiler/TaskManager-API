using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskPriority = TaskManager.Api.Enums.TaskPriority;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManager.Api.Models;

public class TaskItem
{
    public int Id { get; set; }

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

    // Foreign Key for Project
    [Required]
    public int ProjectId { get; set; }
    
    [ForeignKey("ProjectId")]
    public Project Project { get; set; } = null!;

    // Foreign Key for User
    [Required]
    public int AssignedToUserId { get; set; }
    
    [ForeignKey("AssignedToUserId")]
    public User AssignedUser { get; set; } = null!;
}