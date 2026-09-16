using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs.UserDTOs;

public class UserUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public string? Password { get; set; } 

    [MaxLength(20)]
    public string Role { get; set; } = "Employee";
}