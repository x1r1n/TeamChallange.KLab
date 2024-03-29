using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KLab.Application.Core.Abstractions.Data;
using KLab.Application.User.Queries.GetUserImage;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Infrastructure.Core.Services
{
	public class FileService : IFileService
	{
		private readonly BlobContainerClient _containerClient;

		public FileService(BlobContainerClient containerClient)
		{
			_containerClient = containerClient;
		}

		public async Task<Result<GetUserImageQueryResponse>> GetImageAsync(string id)
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

		public async Task<Result> UploadImageAsync(string id, IFormFile image)
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

		public async Task<Result> UpdateImageAsync(string id, IFormFile file)
		{
			var blob = _containerClient.GetBlobClient(id);
			var successDelete = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

			if (!successDelete)
			{
				return Result.Failure(DomainErrors.User.ImageNotFound);
			}

			var updateResult = await UploadImageAsync(id, file);

			return updateResult.IsSuccess
				? Result.Success()
				: Result.Failure(updateResult.Errors);
		}

		public async Task<Result> DeleteImageAsync(string id)
		{
			var blob = _containerClient.GetBlobClient(id);
			var successDelete = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

			return successDelete
				? Result.Success()
				: Result.Failure(DomainErrors.User.ImageNotFound);
		}

		private string SetContentType(string extenstion) =>
			extenstion switch
			{
				".jpg" or ".jpeg" => "image/jpeg",
				".png" => "image/png",
				_ => throw new NotSupportedException($"Extenstion {extenstion} is not supported.")
			};
	}
}
