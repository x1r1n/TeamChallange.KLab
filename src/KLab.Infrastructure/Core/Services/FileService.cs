using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KLab.Application.Core.Abstractions.Data;
using KLab.Application.User.Queries.GetUserImage;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Infrastructure.Core.Services
{
	/// <summary>
	/// Represents the interface for the file service.
	/// </summary>
	public class FileService : IFileService
	{
		private readonly BlobContainerClient _containerClient;

		public FileService(BlobContainerClient containerClient)
		{
			_containerClient = containerClient;
		}

		/// <summary>
		/// Retrieves the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The result containing the user's image</returns>
		public async Task<Result<GetUserImageQueryResponse>> GetUserImageAsync(string id)
		{
			var blob = _containerClient.GetBlobClient(id);

			if (!await blob.ExistsAsync())
			{
				return Result.Failure<GetUserImageQueryResponse>(DomainErrors.User.ImageNotFound);
			}

			var data = await blob.OpenReadAsync();
			var content = await blob.DownloadContentAsync();
			var contentType = content.Value.Details.ContentType;

			return Result.Success(new GetUserImageQueryResponse(id, data, contentType));
		}

		/// <summary>
		/// Set an image for the user asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="file">The image to be set for the user</param>
		/// <returns>The result of operation</returns>
		public async Task<Result> UploadUserImageAsync(string id, IFormFile image)
		{
			var blob = _containerClient.GetBlobClient(id);
			var fileExtenstion = Path.GetExtension(image.FileName);
			var blobHttpHeader = new BlobHttpHeaders
			{
				ContentType = SetContentType(fileExtenstion)
			};

			using (var fileUploadStream = new MemoryStream())
			{
				await image.CopyToAsync(fileUploadStream);

				fileUploadStream.Position = 0;

				await blob.UploadAsync(fileUploadStream, blobHttpHeader);
			}

			return Result.Success();
		}

		/// <summary>
		/// Updates the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="file">The image file to be updated</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> UpdateUserImageAsync(string id, IFormFile file)
		{
			var blob = _containerClient.GetBlobClient(id);
			var successDelete = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

			if (!successDelete)
			{
				return Result.Failure(DomainErrors.User.ImageNotFound);
			}

			var updateResult = await UploadUserImageAsync(id, file);

			return updateResult.IsSuccess
				? Result.Success()
				: Result.Failure(updateResult.Errors);
		}

		/// <summary>
		/// Deletes the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> DeleteUserImageAsync(string id)
		{
			var blob = _containerClient.GetBlobClient(id);
			var successDelete = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

			return successDelete
				? Result.Success()
				: Result.Failure(DomainErrors.User.ImageNotFound);
		}

		/// <summary>
		/// Sets the content type based on the file extension
		/// </summary>
		/// <param name="extension">The file extension</param>
		/// <returns>The content type corresponding to the extension</returns>
		private string SetContentType(string extenstion) =>
			extenstion switch
			{
				".jpg" or ".jpeg" => "image/jpeg",
				".png" => "image/png",
				_ => throw new NotSupportedException($"Extenstion {extenstion} is not supported.")
			};
	}
}
