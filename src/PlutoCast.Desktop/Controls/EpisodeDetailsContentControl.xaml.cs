using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.Models;
using PlutoCast.Desktop.Services;

namespace PlutoCast.Desktop.Controls;

public sealed partial class EpisodeDetailsContentControl : ContentDialog
{
    public static readonly DependencyProperty EpisodeProperty = DependencyProperty.Register(
        nameof(Episode),
        typeof(Episode),
        typeof(EpisodeDetailsContentControl),
        new PropertyMetadata(default(Episode))
    );

    public EpisodeDetailsContentControl()
    {
        DefaultStyleKey = typeof(ContentDialog);
        InitializeComponent();
        XamlRoot = App.MainWindow.Content.XamlRoot;
    }

    public Episode Episode
    {
        get => (Episode)GetValue(EpisodeProperty);
        set => SetValue(EpisodeProperty, value);
    }

    private void BitmapImage_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.ErrorMessage))
            return;

        SubstituteFontIcon.Visibility = Visibility.Visible;
    }

    private void BitmapImage_OnImageOpened(object sender, RoutedEventArgs e)
    {
        SubstituteFontIcon.Visibility = Visibility.Collapsed;
    }
}
