using System;
using System.Windows.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace PlutoCast.Desktop.Controls;

public sealed class CustomFlipView : FlipView
{
    private readonly DispatcherQueue _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
    private readonly DispatcherQueueTimer _timer;
    public static readonly DependencyProperty ItemClickCommandProperty =
        DependencyProperty.Register(
            nameof(ItemClickCommand),
            typeof(ICommand),
            typeof(CustomFlipView),
            new PropertyMetadata(default(ICommand))
        );

    public CustomFlipView()
    {
        DefaultStyleKey = typeof(CustomFlipView);
        _timer = _dispatcherQueue.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(5);
        _timer.Tick += OnTick;
        PointerEntered += OnPointerEntered;
        PointerExited += OnPointerExited;
        PointerPressed += OnPointerPressed;
        Unloaded += OnUnloaded;
        _timer.Start();
    }

    public ICommand? ItemClickCommand
    {
        get => (ICommand)GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType is not PointerDeviceType.Mouse)
        {
            return;
        }

        var currentPointProperties = e.GetCurrentPoint((UIElement)sender).Properties;
        if (
            currentPointProperties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased
            || currentPointProperties.IsRightButtonPressed
        )
        {
            return;
        }
        if (ItemClickCommand is not null && ItemClickCommand.CanExecute(SelectedItem))
        {
            ItemClickCommand.Execute(SelectedItem);
        }
    }

    private void OnPointerExited(object sender, PointerRoutedEventArgs e)
    {
        if (!_timer.IsRunning)
        {
            _timer.Start();
        }
    }

    private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
        }
    }

    private void OnTick(DispatcherQueueTimer sender, object args)
    {
        if (Items.Count > 0)
        {
            SetValue(SelectedIndexProperty, (SelectedIndex + 1) % Items.Count);
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _timer.Tick -= OnTick;
        PointerEntered -= OnPointerEntered;
        PointerExited -= OnPointerExited;
        PointerPressed -= OnPointerPressed;
        Unloaded -= OnUnloaded;
    }
}
