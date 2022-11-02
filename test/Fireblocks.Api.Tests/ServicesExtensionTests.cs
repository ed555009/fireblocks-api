using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fireblocks.Api.Tests;

public class ServicesExtensionTests : BaseServiceTests
{
	private readonly string _settings;

	public ServicesExtensionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
	{
		_settings = JsonSerializer.Serialize(new
		{
			Fireblocks = new
			{
				ApiConfig = new
				{
					FireblocksApiConfig.BaseUrl,
					FireblocksApiConfig.Version,
					FireblocksApiConfig.ApiKey,
					FireblocksApiConfig.SecretKey
				},
				JwtConfig = new
				{
					JwtConfig.Nonce,
					JwtConfig.IssueAt,
					JwtConfig.ExpiredInSeconds
				}
			}
		});
	}

	[Fact]
	public void AddFireblocksApiConfigs_ShouldSucceed()
	{
		// Given
		var services = new ServiceCollection();
		var builder = new ConfigurationBuilder();
		var configuration = builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(_settings))).Build();

		// When
		ServicesExtensions.AddFireblocksApiConfigs(services, configuration);
		var provider = services.BuildServiceProvider();
		var fireblocksApiConfig = provider.GetRequiredService<FireblocksApiConfig>();
		var jwtConfig = provider.GetRequiredService<JwtConfig>();

		// Then
		Assert.Equal(FireblocksApiConfig.BaseUrl, fireblocksApiConfig.BaseUrl);
		Assert.Equal(FireblocksApiConfig.Version, fireblocksApiConfig.Version);
		Assert.Equal(FireblocksApiConfig.ApiKey, fireblocksApiConfig.ApiKey);
		Assert.Equal(FireblocksApiConfig.SecretKey, fireblocksApiConfig.SecretKey);
		Assert.Equal(JwtConfig.Nonce, jwtConfig.Nonce);
		Assert.Equal(JwtConfig.IssueAt, jwtConfig.IssueAt);
		Assert.Equal(JwtConfig.ExpiredInSeconds, jwtConfig.ExpiredInSeconds);
	}

	[Fact]
	public void AddFireblocksApiServices_ShouldSucceed()
	{
		// Given
		var services = new ServiceCollection();
		var builder = new ConfigurationBuilder();
		var configuration = builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(_settings))).Build();

		// When
		ServicesExtensions.AddFireblocksApiServices(services, configuration);

		// Then
		Assert.Contains(services, x => x.ServiceType == typeof(IVaultApi));
		Assert.Contains(services, x => x.ServiceType == typeof(IInternalWalletApi));
		Assert.Contains(services, x => x.ServiceType == typeof(IExternalWalletApi));
		Assert.Contains(services, x => x.ServiceType == typeof(IContractWalletApi));
		Assert.Contains(services, x => x.ServiceType == typeof(IExchangeApi));

		Assert.Contains(services, x => x.ImplementationType == typeof(AuthHeaderHandler));

		Assert.Contains(services, x => x.ServiceType == typeof(IVaultService)
								 && x.ImplementationType == typeof(VaultService));

		Assert.Contains(services, x => x.ServiceType == typeof(IInternalWalletService)
								 && x.ImplementationType == typeof(InternalWalletService));

		Assert.Contains(services, x => x.ServiceType == typeof(IExternalWalletService)
								 && x.ImplementationType == typeof(ExternalWalletService));

		Assert.Contains(services, x => x.ServiceType == typeof(IContractWalletService)
								 && x.ImplementationType == typeof(ContractWalletService));

		Assert.Contains(services, x => x.ServiceType == typeof(IExchangeService)
								 && x.ImplementationType == typeof(ExchangeService));
	}
}