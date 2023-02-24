using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sample.Restaurant.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/*
 * TODO: https://github.com/nblumhardt/serilog-sinks-browserhttp
var appSettings = builder.Configuration.GetSettings<ApplicationSettings>(nameof(ApplicationSettings));
var seqSettings = builder.Configuration.GetSettings<SeqSettings>(nameof(SeqSettings));

builder.Host.ConfigureLogging(appSettings, seqSettings);
*/

// builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var serverApiUrl = builder.Configuration["ServerApiUrl"];
if (string.IsNullOrWhiteSpace(serverApiUrl))
{
    throw new ApplicationException("Could not find API URL");
}

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(serverApiUrl)
    });

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);

#if DEBUG
    options.UseReduxDevTools();
#endif
});

builder.Services.AddScoped<ITableClient>(provider =>
{
    return new TableClient(serverApiUrl, provider.GetService<HttpClient>());
});

await builder.Build().RunAsync();
