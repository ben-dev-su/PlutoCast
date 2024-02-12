using System;
using System.Diagnostics;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Animations.Expressions;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using PlutoCast.Desktop.Helpers;
using PlutoCast.Desktop.ViewModels;
using ECP = Microsoft.UI.Xaml.Hosting.ElementCompositionPreview;
using EF = CommunityToolkit.WinUI.Animations.Expressions.ExpressionFunctions;

namespace PlutoCast.Desktop.Views.Pages;

public sealed partial class PodcastView : Page
{
    private ScrollViewer? _scrollViewer;
    private ScalarNode? _yTranslation;
    private ScalarNode? _progress;
    private ScalarNode? _imageScaleAnimation;
    private ScalarNode? _translateLeftAnimation;
    private ScalarNode? _translateUpAnimation;
    private ScalarNode? _subscribeUpAnimation;
    private Visual? _titleTextVisual;
    private Visual? _headerImageVisual;
    private Visual? _subscribeBtnVisual;
    private Visual? _metaTextVisual;
    private Visual? _descriptionTextVisual;
    private ScalarNode? _opacityKeyFrame;
    private ScalarNode? _opacityAnimation;
    private const int HeroHeaderMinHeight = 100;

    public PodcastView()
    {
        DataContext = App.GetService<PodcastViewModel>();
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    public PodcastViewModel ViewModel => (PodcastViewModel)DataContext;
    private int HeroHeaderMaxHeight => 250;

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _metaTextVisual?.StopAnimation("Opacity");
        _descriptionTextVisual?.StopAnimation("Opacity");
        _metaTextVisual?.StopAnimation("Translation.X");
        _descriptionTextVisual?.StopAnimation("Translation.X");
        _titleTextVisual?.StopAnimation("Translation.X");
        _headerImageVisual?.StopAnimation("Scale.XY");
        _subscribeBtnVisual?.StopAnimation("Translation.Y");
        _subscribeBtnVisual?.StopAnimation("Translation.X");

        _opacityAnimation?.Dispose();
        _opacityKeyFrame?.Dispose();
        _yTranslation?.Dispose();
        _progress?.Dispose();
        _imageScaleAnimation?.Dispose();
        _translateLeftAnimation?.Dispose();
        _translateUpAnimation?.Dispose();
        _subscribeUpAnimation?.Dispose();

        if (_scrollViewer is not null)
        {
            _scrollViewer.ViewChanging -= OnViewChanging;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _scrollViewer = ListView.FindDescendant<ScrollViewer>();
        if (_scrollViewer is null)
        {
            Debug.WriteLine($"No scroll viewer in {nameof(ListView)} found.");
            return;
        }

        _scrollViewer.ViewChanging += OnViewChanging;
        CreateHeaderAnimations(_scrollViewer);
    }

    private void CreateHeaderAnimations(ScrollViewer scrollViewer)
    {
        var scrollViewerPropertySet = ECP.GetScrollViewerManipulationPropertySet(scrollViewer);
        if (scrollViewerPropertySet is null)
        {
            Debug.WriteLine($"{nameof(scrollViewerPropertySet)} is null");
            return;
        }

        StartAnimations(scrollViewerPropertySet);
    }

    private void StartAnimations(CompositionPropertySet scrollViewerPropertySet)
    {
        _yTranslation = scrollViewerPropertySet
            .GetSpecializedReference<ManipulationPropertySetReferenceNode>()
            .Translation
            .Y;
        _progress = EF.Clamp(-_yTranslation / HeroHeaderMinHeight, 0, 1);
        _imageScaleAnimation = EF.Lerp(
            (HeroHeaderMinHeight - 24) / HeaderImage.GetVisual().GetReference().Size.X,
            1,
            1 - _progress
        );

        _opacityKeyFrame = ExpressionValues.Constant.CreateConstantScalar("opacityKeyFrame", 1.0f);
        _opacityAnimation = 1 - EF.Clamp(_progress / _opacityKeyFrame, 0, 1);
        _translateUpAnimation = EF.Lerp(0, -300, _progress);
        _translateLeftAnimation = EF.Lerp(0, -150, _progress);
        _subscribeUpAnimation = EF.Lerp(0, -150, _progress);

        _titleTextVisual = TitleTextGrid.GetVisual();
        _headerImageVisual = HeaderImage.GetVisual();
        _subscribeBtnVisual = SubscribeButton.GetVisual();
        _metaTextVisual = PodcastMetaDataText.GetVisual();
        _descriptionTextVisual = DescriptionText.GetVisual();

        ECP.SetIsTranslationEnabled(PodcastMetaDataText, true);
        ECP.SetIsTranslationEnabled(DescriptionText, true);
        ECP.SetIsTranslationEnabled(TitleTextGrid, true);
        ECP.SetIsTranslationEnabled(HeaderImage, true);
        ECP.SetIsTranslationEnabled(SubscribeButton, true);

        _metaTextVisual.StartAnimation("Opacity", _opacityAnimation);
        _opacityAnimation.SetScalarParameter("opacityKeyFrame", 0.5f);
        _descriptionTextVisual.StartAnimation("Opacity", _opacityAnimation);
        _metaTextVisual.StartAnimation("Translation.X", _translateLeftAnimation);
        _descriptionTextVisual.StartAnimation("Translation.X", _translateLeftAnimation);

        _titleTextVisual.StartAnimation("Translation.X", _translateLeftAnimation);
        _headerImageVisual.StartAnimation(
            "Scale.XY",
            EF.Vector2(_imageScaleAnimation, _imageScaleAnimation)
        );

        _subscribeBtnVisual.StartAnimation("Translation.Y", _subscribeUpAnimation);
        _subscribeBtnVisual.StartAnimation("Translation.X", _translateLeftAnimation);
    }

    private void OnViewChanging(object? sender, ScrollViewerViewChangingEventArgs e)
    {
        HeroHeader.Height = CalculateHeaderHeight(
            e.NextView.VerticalOffset,
            HeroHeaderMinHeight,
            HeroHeaderMaxHeight
        );
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        base.OnNavigatedTo(args);
        ViewModel.OnNavigatedTo(args);
    }

    public static double CalculateHeaderHeight(
        double offset,
        int minHeaderHeight,
        int maxHeaderHeight
    )
    {
        var value = offset / minHeaderHeight;
        var progress = Math.Clamp(value, 0, 1);
        return MathHelper.Lerp(maxHeaderHeight, minHeaderHeight, progress);
    }

    private void AutoSuggestBox_OnTextChanged(
        AutoSuggestBox sender,
        AutoSuggestBoxTextChangedEventArgs args
    )
    {
        ViewModel.OnTextChanged(sender, args);
    }
}
