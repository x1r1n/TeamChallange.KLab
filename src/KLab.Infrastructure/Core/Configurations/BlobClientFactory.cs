using Azure.Storage.Blobs;
using KLab.Infrastructure.Core.Constants;
using Microsoft.Extensions.Configuration;

namespace KLab.Infrastructure.Core.Configurations
{
	public class BlobClientFactory
	{
		private readonly BlobContainerClient _containerClient;

		public BlobContainerClient BlobContainerClient => _containerClient;

		public BlobClientFactory(IConfiguration configuration)
		{
			_containerClient = new BlobContainerClient(
				connectionString: configuration[FileStorageConstants.ConnectionString],
				blobContainerName: configuration[FileStorageConstants.Container]);
		}
	}
}
