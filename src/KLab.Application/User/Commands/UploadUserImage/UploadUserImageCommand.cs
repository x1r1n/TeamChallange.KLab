using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Application.User.Commands.UploadUserImage
{
	public class UploadUserImageCommand : ICommand<Result>
	{
		public string Id { get; init; }
		public IFormFile Image {  get; init; }

		public UploadUserImageCommand(string id, IFormFile image)
		{
			Id = id;
			Image = image;
		}
	}
}
