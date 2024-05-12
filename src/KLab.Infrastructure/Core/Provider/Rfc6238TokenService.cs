using System.Diagnostics;
using System.Security.Cryptography;
using System.Net;

namespace KLab.Infrastructure.Core.Provider
{
	/// <summary>
	/// Provides methods for generating and validating 
	/// Time-Based One-Time Passwords (TOTP) according to RFC 6238
	/// </summary>
	internal class Rfc6238TokenService
	{
		/// <summary>
		/// Represents the Unix epoch time, which is January 1, 1970, 00:00:00 UTC
		/// </summary>
		private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		/// <summary>
		/// Represents the timestep used in TOTP generation, which is 3 minutes
		/// </summary>
		private static readonly TimeSpan _timestep = TimeSpan.FromMinutes(3);

		/// <summary>
		/// Generates a TOTP code based on the provided security token
		/// </summary>
		/// <param name="securityToken">The security token used for generating the TOTP code</param>
		/// <param name="numberOfDigits">The number of digits in the generated TOTP code</param>
		/// <returns>The generated TOTP code</returns>
		/// <exception cref="ArgumentNullException">Thrown when the security token is null</exception>
		public static int GenerateCode(byte[] securityToken, int numberOfDigits)
		{

			if (securityToken == null)
			{
				throw new ArgumentNullException("securityToken");
			}

			var currentTimeStep = GetCurrentTimeStepNumber();

			using (var hashAlgorithm = new HMACSHA1(securityToken))
			{
				var code = ComputeTotp(hashAlgorithm, currentTimeStep, numberOfDigits);
				return code;
			}
		}

		/// <summary>
		/// Validates a TOTP code against the provided security token and parameters
		/// </summary>
		/// <param name="securityToken">The security token used for validating the TOTP code</param>
		/// <param name="code">The TOTP code to validate</param>
		/// <param name="numberOfDigits">The number of digits in the TOTP code</param>
		/// <returns>True if the provided TOTP code is valid, otherwise false</returns>
		/// <exception cref="ArgumentNullException">Thrown when the security token is null</exception>
		public static bool ValidateCode(
			byte[] securityToken, 
			int code, 
			int numberOfDigits)
		{
			if (securityToken == null)
			{
				throw new ArgumentNullException("securityToken");
			}

			var currentTimeStep = GetCurrentTimeStepNumber();

			using (var hashAlgorithm = new HMACSHA1(securityToken))
			{
				for (var i = -2; i <= 2; i++)
				{
					var computedTotp = ComputeTotp(
						hashAlgorithm, 
						(ulong)((long)currentTimeStep + i), 
						numberOfDigits);

					if (computedTotp == code)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Computes a TOTP code based on the provided parameters
		/// </summary>
		/// <param name="hashAlgorithm">The HMAC-based hash algorithm to use</param>
		/// <param name="timestepNumber">The current timestep number</param>
		/// <param name="numberOfDigits">The number of digits in the generated TOTP code</param>
		/// <returns>The computed TOTP code</returns>
		private static int ComputeTotp(
			HashAlgorithm hashAlgorithm, 
			ulong timestepNumber, 
			int numberOfDigits = 4)
		{
			var mod = (int)Math.Pow(10, numberOfDigits);

			var timestepAsBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
			var hash = hashAlgorithm.ComputeHash(timestepAsBytes);

			var offset = hash[hash.Length - 1] & 0xf;

			Debug.Assert(offset + 4 < hash.Length);

			var binaryCode = (hash[offset] & 0x7f) << 24
							 | (hash[offset + 1] & 0xff) << 16
							 | (hash[offset + 2] & 0xff) << 8
							 | (hash[offset + 3] & 0xff);

			var code = binaryCode % mod;

			return code;
		}

		/// <summary>
		/// Gets the current timestep number based on UTC time
		/// </summary>
		/// <returns>The current timestep number</returns>
		private static ulong GetCurrentTimeStepNumber()
		{
			var delta = DateTime.UtcNow - _unixEpoch;
			return (ulong)(delta.Ticks / _timestep.Ticks);
		}
	}
}
