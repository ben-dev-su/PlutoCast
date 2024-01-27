using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using PlutoCast.Desktop.ViewModels;
using Windows.ApplicationModel;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class ShellView : Page
{
    public ShellView()
    {
        DataContext = App.GetService<ShellViewModel>();
        InitializeComponent();
        ContentFrame.Navigate(typeof(DiscoverView), null, new ContinuumNavigationTransitionInfo());
    }

    public ShellViewModel ViewModel => (ShellViewModel)DataContext;

    private void ShellView_OnLoading(FrameworkElement sender, object args)
    {
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(TitleBar);
        App.MainWindow.Activated += MainWindowOnActivated;
    }

    private void MainWindowOnActivated(object sender, WindowActivatedEventArgs args)
    {
        var resource =
            args.WindowActivationState == WindowActivationState.Deactivated
                ? "WindowCaptionForegroundDisabled"
                : "WindowCaptionForeground";

        TitleBarTextBlock.Foreground = (SolidColorBrush)Application.Current.Resources[resource];
        TitleBarTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
    }
}
