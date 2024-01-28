using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using PlutoCast.Desktop.Interfaces;
using PlutoCast.Desktop.ViewModels;
using PlutoCast.Desktop.Views.Pages;

namespace PlutoCast.Desktop.Services;

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, Type> _views = [];
    private NavigationViewItem? _selectedItem;
    private Frame? _frame;
    private NavigationView? _navigationView;
    private readonly Stack<NavigationViewItem> _backStack = [];

    public NavigationService()
    {
        RegisterView<DiscoverView, DiscoverViewModel>();
        RegisterView<PlaylistsView, PlaylistsViewModel>();
        RegisterView<QueueView, QueueViewModel>();
        RegisterView<SettingsView, SettingsViewModel>();
        RegisterView<SubscriptionsView, SubscriptionsViewModel>();
    }

    public NavigationView? NavigationView
    {
        get => _navigationView;
        set
        {
            _navigationView = value;
            RegisterNavigationViewEvents();
        }
    }

    public Frame? Frame
    {
        get => _frame;
        set
        {
            _frame = value;
            RegisterFrameEvents();
        }
    }

    public bool CanGoBack => Frame is not null && Frame.CanGoBack;
    public bool CanGoForward => Frame is not null && Frame.CanGoForward;

    public void Initialize(NavigationView navigationView, Frame frame)
    {
        Frame = frame;
        NavigationView = navigationView;
        var selectedItem = NavigationView
            .MenuItems
            .OfType<NavigationViewItem>()
            .First(x => (string)x.Tag == nameof(DiscoverViewModel));
        _backStack.Push(selectedItem);
        Navigate(nameof(DiscoverViewModel));
    }

    public NavigationViewItem? GetSelectedItem(NavigationMode navigationMode)
    {
        if (!_backStack.Any())
        {
            return null;
        }

        NavigationViewItem? item;
        if (navigationMode == NavigationMode.Back)
        {
            _ = _backStack.Pop();
            if (_backStack.TryPeek(out item))
                return item;
        }

        if (_backStack.TryPeek(out item))
        {
            return item;
        }

        return null;
    }

    public event NavigatedEventHandler? Navigated;

    public Type GetViewType(string tag)
    {
        Type? pageType;
        lock (_views)
        {
            if (!_views.TryGetValue(tag, out pageType))
            {
                throw new InvalidOperationException(
                    $"View not with the following tag not found: {tag}. Register view in PageService."
                );
            }
        }
        return pageType;
    }

    public void GoBack()
    {
        if (CanGoBack)
        {
            Frame?.GoBack();
        }
    }

    public void GoForward()
    {
        if (CanGoForward)
        {
            Frame?.GoForward();
        }
    }

    public bool Navigate(
        string viewTag,
        object? parameter = null,
        NavigationTransitionInfo? transitionInfo = null
    )
    {
        var viewType = GetViewType(viewTag);

        return Frame is not null && Frame.Navigate(viewType, parameter, transitionInfo);
    }

    private void OnNavigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);

    private void RegisterFrameEvents()
    {
        if (_frame is null)
        {
            throw new InvalidOperationException(
                "Frame must be initialized before registering events."
            );
        }

        _frame.Navigated += OnNavigated;
    }

    private void RegisterNavigationViewEvents()
    {
        if (_navigationView is null)
        {
            throw new InvalidOperationException(
                "NavigationView must be initialized before registering events."
            );
        }

        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer is not NavigationViewItem selectedItem)
        {
            throw new InvalidOperationException(
                "InvokedItemContainer is not of type NavigationViewItem."
            );
        }

        _backStack.Push(selectedItem);

        if (args.IsSettingsInvoked)
        {
            _ = Navigate(nameof(SettingsViewModel));
        }
        else
        {
            NavigateToInvokedItem(selectedItem);
        }
    }

    private void NavigateToInvokedItem(NavigationViewItem? selectedItem)
    {
        if (selectedItem?.Tag is not string tag)
        {
            throw new InvalidOperationException(
                $"The Tag property is either null or not a string. Check the NavigationView on the {nameof(ShellView)}."
            );
        }

        _ = Navigate(tag);
    }

    private void OnBackRequested(
        NavigationView sender,
        NavigationViewBackRequestedEventArgs args
    ) => GoBack();

    private void RegisterView<TView, TViewModel>()
        where TView : Page
        where TViewModel : BaseViewModel
    {
        lock (_views)
        {
            var viewModelKey = typeof(TViewModel).Name;
            var viewType = typeof(TView);

            if (_views.ContainsKey(viewModelKey))
            {
                throw new ArgumentException($"The key: \"{viewModelKey}\" already exist");
            }

            if (_views.ContainsValue(viewType))
            {
                throw new ArgumentException(
                    $"Type \"{viewType}\" already exist with following key {_views.First(p => p.Value == viewType).Key}"
                );
            }

            _views.Add(viewModelKey, viewType);
        }
    }
}
