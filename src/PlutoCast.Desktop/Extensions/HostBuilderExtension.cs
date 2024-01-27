using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlutoCast.Desktop.ViewModels;
using PlutoCast.Desktop.Views.Pages;

namespace PlutoCast.Desktop.Extensions;

public static class HostBuilderExtension
{
    public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureAppConfiguration(configuration =>
        {
            configuration.AddJsonFile("appsettings.json", optional: false);
            configuration.AddEnvironmentVariables();
        });
    }

    public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices(
            (context, services) =>
            {
                // Views and ViewModels
                _ = services.AddTransient<DiscoverView>();
                _ = services.AddTransient<DiscoverViewModel>();
                _ = services.AddTransient<PlaylistsView>();
                _ = services.AddTransient<PlaylistsViewModel>();
                _ = services.AddTransient<QueueView>();
                _ = services.AddTransient<QueueViewModel>();
                _ = services.AddTransient<SettingsView>();
                _ = services.AddTransient<SettingsViewModel>();
                _ = services.AddTransient<ShellView>();
                _ = services.AddTransient<ShellViewModel>();
                _ = services.AddTransient<SubscriptionsView>();
                _ = services.AddTransient<SubscriptionsViewModel>();
            }
        );
    }

    public static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureLogging(ConfigureLogging());
    }

    private static Action<ILoggingBuilder> ConfigureLogging()
    {
        return builder =>
        {
            builder.ClearProviders();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Debug);
        };
    }
}
