using System.ComponentModel.DataAnnotations;

namespace SmartGallery.Shared;

public class LoginViewModel
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string? Password { get; set; }
}

