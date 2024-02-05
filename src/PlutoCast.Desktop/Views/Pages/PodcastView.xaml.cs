using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class PodcastView : Page
{
    public PodcastView()
    {
        DataContext = App.GetService<PodcastViewModel>();
        InitializeComponent();
    }

    public PodcastViewModel ViewModel => (PodcastViewModel)DataContext;
}
