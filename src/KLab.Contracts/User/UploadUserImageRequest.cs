using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.User
{
	/// <summary>
	/// Represents a request for uploading user image
	/// </summary>
	public class UploadUserImageRequest
	{
		[Required]
		public IFormFile? Image { get; set; }
	}
}
