using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.ViewModels;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class QueueView : Page
{
    public QueueView()
    {
        DataContext = App.GetService<QueueViewModel>();
        InitializeComponent();
    }

    public QueueViewModel ViewModel => (QueueViewModel)DataContext;
}
