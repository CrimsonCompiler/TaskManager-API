using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class ProjectUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
}