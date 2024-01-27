using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class SubscriptionsView : Page
{
    public SubscriptionsView()
    {
        DataContext = App.GetService<SubscriptionsViewModel>();
        InitializeComponent();
    }

    public SubscriptionsViewModel ViewModel => (SubscriptionsViewModel)DataContext;
}
