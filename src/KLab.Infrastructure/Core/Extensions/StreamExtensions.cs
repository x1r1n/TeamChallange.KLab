using System.Text;

namespace KLab.Infrastructure.Core.Extensions
{
	internal static class StreamExtensions
	{
		internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

		public static BinaryWriter CreateWriter(this Stream stream)
			=> new BinaryWriter(stream, DefaultEncoding, true);

		public static void Write(this BinaryWriter writer, DateTimeOffset value)
		{
			writer.Write(value.UtcTicks);
		}
	}
}