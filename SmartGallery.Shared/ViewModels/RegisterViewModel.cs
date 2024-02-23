using System;
using System.ComponentModel.DataAnnotations;

namespace SmartGallery.Shared
{
	public class RegisterViewModel
	{
		[Required]
		[StringLength(50)]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		[StringLength(50,MinimumLength =8)]
		public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(200)]
        public string? Address { get; set; }
    }
}

