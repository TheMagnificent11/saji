using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Testing;

namespace Lewee.IntegrationTests;

/// <summary>
/// Test Host
/// </summary>
public class TestHost : IDisposable
{
    private readonly IHost host;
    private readonly TestServer testServer;

    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestHost"/> class.
    /// </summary>
    /// <param name="configureAppConfiguration">Action to configure app configuration</param>
    /// <param name="configureServices">Action to configure services</param>
    /// <param name="configureApplication">Action to configure application pipeline</param>
    public TestHost(
        Action<IConfigurationBuilder> configureAppConfiguration,
        Action<WebHostBuilderContext, IServiceCollection> configureServices,
        Action<IApplicationBuilder> configureApplication)
    {
        this.host = new HostBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseTestServer();

                webBuilder
                    .ConfigureAppConfiguration(configureAppConfiguration)
                    .ConfigureServices((context, services) =>
                    {
                        services.AddFakeLogging();
                        configureServices(context, services);
                    })
                    .Configure(configureApplication);
            })
            .Start();

        this.testServer = this.host.GetTestServer();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets a HTTP client to make HTTP calls to this test host
    /// </summary>
    /// <returns>HTTP client</returns>
    public HttpClient GetHttpClient() => this.host.GetTestClient();

    /// <summary>
    /// Gets the logging sink from the dependency injection container
    /// </summary>
    /// <returns>Fake log collector</returns>
    public FakeLogCollector GetLoggingSink()
    {
        return this.host
            .Services
            .GetFakeLogCollector();
    }

    /// <summary>
    /// Dispose disposal resources
    /// </summary>
    /// <param name="disposing">Whether disposing</param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.disposedValue)
        {
            return;
        }

        if (disposing)
        {
            this.testServer.Dispose();
            this.host.Dispose();
        }

        this.disposedValue = true;
    }
}
