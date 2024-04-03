using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.User
{
	/// <summary>
	/// Represents a request for updating user image
	/// </summary>
	public class UpdateUserImageRequest
	{
		[Required]
		public IFormFile? Image { get; set; }
	}
}
