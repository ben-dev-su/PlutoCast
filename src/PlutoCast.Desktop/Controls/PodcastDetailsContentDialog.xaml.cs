using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.Models;

namespace PlutoCast.Desktop.Controls;

public sealed partial class PodcastDetailsContentDialog : ContentDialog
{
    public static readonly DependencyProperty PodcastProperty = DependencyProperty.Register(
        nameof(Podcast),
        typeof(TrendingPodcast),
        typeof(PodcastDetailsContentDialog),
        new PropertyMetadata(default(TrendingPodcast))
    );

    public PodcastDetailsContentDialog()
    {
        DefaultStyleKey = typeof(ContentDialog);
        InitializeComponent();
        XamlRoot = App.MainWindow.Content.XamlRoot;
    }

    public TrendingPodcast Podcast
    {
        get => (TrendingPodcast)GetValue(PodcastProperty);
        set => SetValue(PodcastProperty, value);
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
