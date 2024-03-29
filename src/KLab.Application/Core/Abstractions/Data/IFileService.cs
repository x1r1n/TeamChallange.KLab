using KLab.Application.User.Queries.GetUserImage;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Application.Core.Abstractions.Data
{
	public interface IFileService
	{
		Task<Result<GetUserImageQueryResponse>> GetImageAsync(string id);
		Task<Result> UploadImageAsync(string id, IFormFile file);
		Task<Result> UpdateImageAsync(string id, IFormFile file);
		Task<Result> DeleteImageAsync(string id);
	}
}
