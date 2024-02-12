using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PlutoCast.Desktop.Models;
using PlutoCast.Desktop.Services;

namespace PlutoCast.Desktop.Controls;

public sealed partial class CustomEpisodeItem : UserControl
{
    public static readonly DependencyProperty EpisodeProperty = DependencyProperty.Register(
        nameof(Episode),
        typeof(Episode),
        typeof(CustomEpisodeItem),
        new PropertyMetadata(default(Episode))
    );

    public CustomEpisodeItem()
    {
        InitializeComponent();
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

    private async void DetailsFlyout_OnClick(object sender, RoutedEventArgs e)
    {
        EpisodeDetailsContentControl dialog =
            new()
            {
                Episode = Episode,
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.None,
            };

        var result = await dialog.ShowAsync();
    }
}
