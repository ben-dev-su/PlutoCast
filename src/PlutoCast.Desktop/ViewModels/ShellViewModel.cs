using CommunityToolkit.Mvvm.ComponentModel;

namespace PlutoCast.Desktop.ViewModels;

[ObservableObject]
public partial class ShellViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool _canGoBack;
}
