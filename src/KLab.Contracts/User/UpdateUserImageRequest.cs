using Microsoft.AspNetCore.Http;

namespace KLab.Contracts.User
{
	public class UpdateUserImageRequest
	{
		public IFormFile? Image { get; set; }
	}
}
