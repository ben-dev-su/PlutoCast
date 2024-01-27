using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class SettingsView : Page
{
    public SettingsView()
    {
        DataContext = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    public SettingsViewModel ViewModel => (SettingsViewModel)DataContext;
}
