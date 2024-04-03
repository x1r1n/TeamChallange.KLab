using KLab.Application.User.Queries.GetUserImage;
using KLab.Domain.Core.Primitives.ResultModel;
using Microsoft.AspNetCore.Http;

namespace KLab.Application.Core.Abstractions.Data
{
	/// <summary>
	/// Represents the interface for the file service.
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Retrieves the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The result containing the user's image</returns>
		Task<Result<GetUserImageQueryResponse>> GetUserImageAsync(string id);

		/// <summary>
		/// Set an image for the user asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="file">The image to be set for the user</param>
		/// <returns>The result of operation</returns>
		Task<Result> UploadUserImageAsync(string id, IFormFile file);

		/// <summary>
		/// Updates the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="file">The image file to be updated</param>
		/// <returns>The result of the operation</returns>
		Task<Result> UpdateUserImageAsync(string id, IFormFile file);

		/// <summary>
		/// Deletes the user's image asynchronously
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The result of the operation</returns>
		Task<Result> DeleteUserImageAsync(string id);
	}
}
