namespace KLab.Application.User.Queries.GetUserImage
{
	public class GetUserImageQueryResponse
	{
		public string Name { get; init; }
		public Stream Content { get; init; }
		public string ContentType { get; init; }

		public GetUserImageQueryResponse(string name, Stream content, string contentType)
		{
			Name = name;
			Content = content;
			ContentType = contentType;
		}
	}
}
