﻿using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Application.User.Commands.UpdateUserImage
{
	/// <summary>
	/// Represents a command for updating a user's image.
	/// </summary>
	public class UpdateUserImageCommand : ICommand<Result>
	{
		public string Id { get; init; }
		public IFormFile Image { get; init; }

		public UpdateUserImageCommand(string id, IFormFile image)
		{
			Id = id;
			Image = image;
		}
	}
}
