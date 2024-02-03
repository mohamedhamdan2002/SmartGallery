using System;
using Microsoft.AspNetCore.Identity;

namespace SmartGallery.Server.Models
{
	public class Customer :IdentityUser
	{
		public string? Address { get; set; }
	}
}

