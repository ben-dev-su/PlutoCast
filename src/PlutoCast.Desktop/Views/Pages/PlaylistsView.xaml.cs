using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class PlaylistsView : Page
{
    public PlaylistsView()
    {
        DataContext = App.GetService<PlaylistsViewModel>();
        InitializeComponent();
    }

    public PlaylistsViewModel ViewModel => (PlaylistsViewModel)DataContext;
}
