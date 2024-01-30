using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace PlutoCast.Desktop.Controls;

public sealed partial class CarouselItemControl : UserControl
{
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(CarouselItemControl),
        new PropertyMetadata(default(string))
    );

    public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register(
        nameof(Author),
        typeof(string),
        typeof(CarouselItemControl),
        new PropertyMetadata(default(string))
    );

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description),
        typeof(string),
        typeof(CarouselItemControl),
        new PropertyMetadata(default(string))
    );

    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        nameof(Image),
        typeof(Uri),
        typeof(CarouselItemControl),
        new PropertyMetadata(default(Uri))
    );

    public CarouselItemControl()
    {
        InitializeComponent();
    }

    public string Author
    {
        get => (string)GetValue(AuthorProperty);
        set => SetValue(AuthorProperty, value);
    }

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public Uri Image
    {
        get => (Uri)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    private void BitmapImage_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        SubstituteIcon.Visibility = Visibility.Visible;
    }
}
