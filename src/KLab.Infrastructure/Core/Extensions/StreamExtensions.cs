using System.Text;

namespace KLab.Infrastructure.Core.Extensions
{
	/// <summary>
	/// Provides extension methods for working with streams
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// The default encoding used for writing to streams
		/// </summary>
		internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

		/// <summary>
		/// Creates a binary writer for the stream using the default encoding
		/// </summary>
		/// <param name="stream">The stream to write</param>
		/// <returns>A binary writer instance</returns>
		public static BinaryWriter CreateWriter(this Stream stream)
			=> new BinaryWriter(stream, DefaultEncoding, true);

		/// <summary>
		/// Writes a DateTimeOffset value to the stream
		/// </summary>
		/// <param name="writer">The binary writer</param>
		/// <param name="value">The DateTimeOffset value to write</param>
		public static void Write(this BinaryWriter writer, DateTimeOffset value)
		{
			writer.Write(value.UtcTicks);
		}
	}
}