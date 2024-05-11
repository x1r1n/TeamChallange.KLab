namespace KLab.Api.Contracts
{
	/// <summary>
	/// Represents the origins from which an client request can originate
	/// </summary>
	public static class Origins
	{
		public static string Local => "Origins:Local";
		public static string Network => "Origins:Network";
		public static string Production => "Origins:Production";
	}
}
