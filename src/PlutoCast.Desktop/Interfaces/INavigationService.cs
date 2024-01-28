using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace PlutoCast.Desktop.Interfaces;

public interface INavigationService
{
    Type GetViewType(string tag);
    Frame? Frame { get; set; }
    bool CanGoBack { get; }
    bool CanGoForward { get; }
    void GoBack();
    bool Navigate(
        string viewTag,
        object? parameter = null,
        NavigationTransitionInfo? transitionInfo = null
    );

    void GoForward();
    void Initialize(NavigationView navigationView, Frame frame);
    NavigationViewItem? GetSelectedItem(NavigationMode navigationMode);

    event NavigatedEventHandler Navigated;
}
