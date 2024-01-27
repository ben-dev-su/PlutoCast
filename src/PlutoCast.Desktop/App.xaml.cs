using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using PlutoCast.Desktop.Extensions;
using PlutoCast.Desktop.Views;
using PlutoCast.Desktop.Views.Pages;
using WinUIEx;

namespace PlutoCast.Desktop;

public partial class App : Application
{
    private readonly IHost _host;
    private readonly ILogger<App> _logger;

    public App()
    {
        InitializeComponent();
        _host = CreateHostBuilder().Build();
        _logger = GetService<ILogger<App>>();
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static T GetService<T>()
        where T : class
    {
        if ((Current as App)?._host.Services.GetRequiredService<T>() is not { } service)
        {
            throw new InvalidOperationException($"{typeof(T)} needs to be registered in ...");
        }

        return service;
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .AddConfiguration()
            .AddLogging()
            .AddServices();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        Activate();
        UnhandledException += OnUnhandledException;
        MainWindow.Closed += MainWindowOnClosed;
    }

    private async void MainWindowOnClosed(object sender, WindowEventArgs args)
    {
        await _host.StopAsync();
        _host.Dispose();
        Current.Exit();
    }

    private void OnUnhandledException(
        object sender,
        Microsoft.UI.Xaml.UnhandledExceptionEventArgs e
    )
    {
        _logger.LogError(e.Exception, e.Message);
    }

    private async void Activate()
    {
        await _host.StartAsync();
        if (MainWindow.Content is null)
        {
            UIElement shell = GetService<ShellView>();
            MainWindow.Content = shell;
        }

        MainWindow.Activate();
    }
}
