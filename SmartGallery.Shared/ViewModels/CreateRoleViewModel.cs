using System.ComponentModel.DataAnnotations;

namespace SmartGallery.Shared.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string RoleName { get; set; }
	}
}

