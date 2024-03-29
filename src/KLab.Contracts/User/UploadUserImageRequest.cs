using Microsoft.AspNetCore.Http;

namespace KLab.Contracts.User
{
	public class UploadUserImageRequest
	{
		public IFormFile? Image { get; set; }
	}
}
