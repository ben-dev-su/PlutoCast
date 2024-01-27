using CommunityToolkit.Mvvm.ComponentModel;

namespace PlutoCast.Desktop.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _canGoBack;
}
