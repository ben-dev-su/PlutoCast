using Microsoft.UI.Xaml.Media;
using WinUIEx;
using Windows.ApplicationModel;

namespace PlutoCast.Desktop.Views;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();
        Configure();
    }

    private void Configure()
    {
        Content = null;
        SystemBackdrop = new MicaBackdrop();
        Title = AppInfo.Current.DisplayInfo.DisplayName;
    }
}
