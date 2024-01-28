using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using PlutoCast.Desktop.Interfaces;

namespace PlutoCast.Desktop.ViewModels;

[ObservableObject]
public partial class ShellViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool _canGoBack;

    [ObservableProperty]
    private NavigationViewItem? _selectedItem;

    public ShellViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
    }

    public INavigationService NavigationService { get; }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        CanGoBack = NavigationService.CanGoBack;
        var selectedItem = NavigationService.GetSelectedItem(e.NavigationMode);
        if (selectedItem is not null)
        {
            SelectedItem = selectedItem;
        }
    }
}
