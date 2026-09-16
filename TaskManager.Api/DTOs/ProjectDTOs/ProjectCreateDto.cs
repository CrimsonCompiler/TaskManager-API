using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class ProjectCreateDto
{
    [Required(ErrorMessage = "Project name is required")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
}