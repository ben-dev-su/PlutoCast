using System;
using System.Collections;
using System.Windows.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace PlutoCast.Desktop.Controls;

public sealed partial class CarouselControl : UserControl
{
    private ScrollViewer? _scrollViewer;

    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource),
        typeof(IList),
        typeof(CarouselControl),
        new PropertyMetadata(default(IList))
    );

    public static readonly DependencyProperty ItemClickCommandProperty =
        DependencyProperty.Register(
            nameof(ItemClickCommand),
            typeof(ICommand),
            typeof(CarouselControl),
            new PropertyMetadata(default(ICommand))
        );

    public CarouselControl()
    {
        InitializeComponent();
        Unloaded += OnUnloaded;
    }

    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ICommand? ItemClickCommand
    {
        get => (ICommand)GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }

    private void BackwardsButton_OnClick(object sender, RoutedEventArgs e)
    {
        MoveCarousel(-1);
    }

    private void ForwardsButton_OnClick(object sender, RoutedEventArgs e)
    {
        MoveCarousel(1);
    }

    private void MoveCarousel(int direction)
    {
        if (ListView.ContainerFromIndex(0) is not SelectorItem containerFromIndex)
        {
            return;
        }

        var offsetChange =
            containerFromIndex.ActualWidth
            * (int)Math.Floor(ListView.ActualWidth / containerFromIndex.ActualWidth);
        var newHorizontalOffset = _scrollViewer?.HorizontalOffset + (offsetChange * direction);

        _ = _scrollViewer?.ChangeView(newHorizontalOffset, null, null, false);
    }

    private void CarouselControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        _scrollViewer =
            ListView.FindDescendant<ScrollViewer>()
            ?? throw new InvalidOperationException("Couldn't find ScrollViewer");

        _scrollViewer.ViewChanging += ScrollViewerOnViewChanging;
    }

    private void ScrollViewerOnViewChanging(object? sender, ScrollViewerViewChangingEventArgs e)
    {
        if (e.FinalView.HorizontalOffset < 1)
        {
            BackwardsButton.IsEnabled = false;
        }
        else if (e.FinalView.HorizontalOffset > 1)
        {
            BackwardsButton.IsEnabled = true;
        }

        if (e.FinalView.HorizontalOffset > _scrollViewer?.ScrollableWidth - 1)
        {
            ForwardsButton.IsEnabled = false;
        }
        else if (e.FinalView.HorizontalOffset < _scrollViewer?.ScrollableWidth - 1)
        {
            ForwardsButton.IsEnabled = true;
        }
    }

    private void ListView_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (e.NewSize.Width > _scrollViewer?.ScrollableWidth - 1)
        {
            ForwardsButton.IsEnabled = false;
        }
        else if (e.NewSize.Width < _scrollViewer?.ScrollableWidth - 1)
        {
            ForwardsButton.IsEnabled = true;
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_scrollViewer is not null)
        {
            _scrollViewer.ViewChanging -= ScrollViewerOnViewChanging;
        }
    }

    private void OnItemClick(object sender, ItemClickEventArgs e)
    {
        if (ItemClickCommand is not null && ItemClickCommand.CanExecute(e.ClickedItem))
        {
            ItemClickCommand.Execute(e.ClickedItem);
        }
    }
}
