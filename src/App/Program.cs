using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using App.Commands;
using App.Configuration;
using App.Extensions;
using App.Services.Console;
using App.Services.Ip;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace App;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            return await CreateHostBuilder(args).RunCommandLineApplicationAsync<ToolCommand>(args);
        }
        catch (Exception ex)
        {
            ConsoleService.RenderAnyException(ex);
            return Settings.ExitCode.Ko;
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile();
                config.AddUserSecrets();
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .ConfigureLogging((_, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddDefaultLogger();
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services
                    .Configure<Settings>(hostingContext.Configuration.GetSection(nameof(Settings)))
                    .PostConfigure<Settings>(settings =>
                    {
                        var distinctUrls = settings.SourceUrls
                            .Where(url => !string.IsNullOrWhiteSpace(url))
                            .Distinct()
                            .ToArray();
                        settings.SourceUrls = distinctUrls;
                    });
                    
                services.AddTransient<IConsoleService, ConsoleService>();
                services.AddTransient<ToolCommand>();
                services
                    .AddHttpClient<IIpService, IpService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(2))
                    .AddPolicyHandler(GetRetryPolicy());
            })
            .AddSerilog();
    
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        const int maxRetry = 3;
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(maxRetry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}