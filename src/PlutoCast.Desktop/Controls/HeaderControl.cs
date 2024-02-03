using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace PlutoCast.Desktop.Controls;

public sealed class HeaderControl : ContentControl
{
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(HeaderControl),
        new PropertyMetadata(default(object))
    );

    public HeaderControl()
    {
        DefaultStyleKey = typeof(HeaderControl);
    }

    public object Header
    {
        get => (object)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
}
