using Microsoft.AspNetCore.Identity;
namespace SmartGallery.Server.Models
{
	public class Customer : IdentityUser
	{
		public string? Address { get; set; }
		public ICollection<Review> Reviews { get; set; } = new List<Review>();
		public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}

