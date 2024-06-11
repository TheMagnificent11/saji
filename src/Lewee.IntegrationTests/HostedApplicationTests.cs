using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace Lewee.IntegrationTests;

/// <summary>
/// Hosted Application Tests
/// </summary>
public abstract class HostedApplicationTests : IAsyncLifetime
{
    private TestHost testHost;

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        this.testHost = new TestHost(
            this.SetupAppConfiguration,
            this.ConfigureServices,
            this.ConfigureApplication);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DisposeAsync()
    {
        this.testHost?.Dispose();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sets up the application configuration
    /// </summary>
    /// <param name="configurationBuilder">Configuration builder</param>
    protected virtual void SetupAppConfiguration(IConfigurationBuilder configurationBuilder)
    {
    }

    /// <summary>
    /// Configures service dependencies
    /// </summary>
    /// <param name="context">Web host builder context</param>
    /// <param name="services">Service collection</param>
    protected abstract void ConfigureServices(WebHostBuilderContext context, IServiceCollection services);

    /// <summary>
    /// Configures the application pipeline
    /// </summary>
    /// <param name="app">Application builder</param>
    protected abstract void ConfigureApplication(IApplicationBuilder app);

    /// <summary>
    /// Gets a test HTTP client to call make HTTP calls to test host
    /// </summary>
    /// <returns>HTTP client</returns>
    protected HttpClient GetTestHttpClient() => this.testHost.GetHttpClient();

    /// <summary>
    /// Executes a HTTP request
    /// </summary>
    /// <param name="httpMethod">HTTP method</param>
    /// <param name="requestPath">Request path</param>
    /// <param name="body">Request body</param>
    /// <returns>An asynchronous task containing a HTTP response message</returns>
    protected async Task<HttpResponseMessage> ExecuteHttpRequest(
        HttpMethod httpMethod,
        string requestPath,
        object? body = null)
    {
        using (var request = new HttpRequestMessage(httpMethod, requestPath))
        using (var client = this.GetTestHttpClient())
        {
            if (body != null)
            {
                request.Content = JsonContent.Create(body);
            }

            return await client.SendAsync(request);
        }
    }

    /// <summary>
    /// Deserializes a HTTP response
    /// </summary>
    /// <typeparam name="T">Response body type</typeparam>
    /// <param name="response">Response to deserialize</param>
    /// <param name="isSuccess">Whether the response was expected successful</param>
    /// <returns>The deserialized data</returns>
    protected virtual async Task<T?> DeserializeResponse<T>(HttpResponseMessage response, bool isSuccess = true)
    {
        if (isSuccess)
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    /// <summary>
    /// Gets the logging sink
    /// </summary>
    /// <returns>Fake log collector</returns>
    protected FakeLogCollector GetLoggingSink()
    {
        return this.testHost.GetLoggingSink();
    }
}
