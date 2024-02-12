using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.Models;

namespace PlutoCast.Desktop.Controls;

public sealed partial class CarouselItemControl : UserControl
{
    public static readonly DependencyProperty PodcastProperty = DependencyProperty.Register(
        nameof(Podcast),
        typeof(TrendingPodcast),
        typeof(CarouselItemControl),
        new PropertyMetadata(default(TrendingPodcast))
    );

    public CarouselItemControl()
    {
        InitializeComponent();
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

        SubstituteIcon.Visibility = Visibility.Visible;
        Shimmer.IsActive = false;
        Shimmer.Visibility = Visibility.Collapsed;
    }

    private void BitmapImage_OnImageOpened(object sender, RoutedEventArgs e)
    {
        SubstituteIcon.Visibility = Visibility.Collapsed;
        Shimmer.IsActive = false;
        Shimmer.Visibility = Visibility.Collapsed;
    }
}
