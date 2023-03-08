using Fluxor;
using Lewee.Blazor.Messaging;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Sample.Restaurant.App;
using Sample.Restaurant.App.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var serverApiUrl = builder.Configuration["ServerApiUrl"];
if (string.IsNullOrWhiteSpace(serverApiUrl))
{
    throw new ApplicationException("Could not find API URL");
}

/*
 * TODO: https://github.com/nblumhardt/serilog-sinks-browserhttp
var appSettings = builder.Configuration.GetSettings<ApplicationSettings>(nameof(ApplicationSettings));
var seqSettings = builder.Configuration.GetSettings<SeqSettings>(nameof(SeqSettings));

builder.Host.ConfigureLogging(appSettings, seqSettings);

var levelSwitch = new LoggingLevelSwitch();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .WriteTo.BrowserHttp($"{serverApiUrl}/ingest", controlLevelSwitch: levelSwitch)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
*/

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

builder.Services.ConfigureMessageReceiver<MessageToActionMapper>(serverApiUrl);

builder.Services.AddScoped<ITableClient>(provider =>
{
    return new TableClient(serverApiUrl, provider.GetService<HttpClient>());
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
