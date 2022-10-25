namespace Fireblocks.Api.Interfaces;

/// <summary>
/// Fireblocks Api configuration
/// </summary>
public interface IFireblocksApiConfig
{
	/// <summary>
	/// Api base url
	/// </summary>
	string BaseUrl { get; set; }

	/// <summary>
	/// The API Key to be provided to you by Fireblocks
	/// </summary>
	string? ApiKey { get; set; }

	/// <summary>
	/// RSA 4096 private key (stored in fireblocks_secret.key)<br/>
	/// https://docs.fireblocks.com/api/#issuing-api-credentials
	/// </summary>
	string? SecretKey { get; set; }
}
