using System;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace PlutoCast.Desktop.Controls;

public sealed class CustomFlipView : FlipView
{
    private readonly DispatcherQueue _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
    private readonly DispatcherQueueTimer _timer;

    public CustomFlipView()
    {
        DefaultStyleKey = typeof(CustomFlipView);
        _timer = _dispatcherQueue.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(5);
        _timer.Tick += OnTick;
        PointerEntered += OnPointerEntered;
        PointerExited += OnPointerExited;
        _timer.Start();
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
        SetValue(SelectedIndexProperty, (SelectedIndex + 1) % Items.Count);
    }
}
