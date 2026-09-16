using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name maximum length is 100")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is not valid")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(60, ErrorMessage = "Invalid password hash length")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public string PasswordSalt { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string? Role { get; set; }
}