using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class DiscoverView : Page
{
    public DiscoverView()
    {
        DataContext = App.GetService<DiscoverViewModel>();
        InitializeComponent();
    }

    public DiscoverViewModel ViewModel => (DiscoverViewModel)DataContext;
}
