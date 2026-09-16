using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Models;

public class Project
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}