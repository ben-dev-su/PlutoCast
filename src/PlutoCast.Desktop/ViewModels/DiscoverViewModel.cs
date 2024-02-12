using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Animation;
using PlutoCast.Desktop.Interfaces;
using PlutoCast.Desktop.Models;
using PlutoCast.Desktop.Services;

namespace PlutoCast.Desktop.ViewModels;

[ObservableObject]
public partial class DiscoverViewModel(
    BogusService bogusService,
    INavigationService navigationService
) : BaseViewModel
{
    public List<TrendingPodcast> TopTrendingPodcasts => bogusService.TopTrendingPodcasts;
    public List<TrendingPodcast> NewsTrendingPodcasts => bogusService.NewsTrendingPodcasts;
    public List<TrendingPodcast> ComedyTrendingPodcasts => bogusService.ComedyTrendingPodcasts;
    public List<TrendingPodcast> ScienceTrendingPodcasts => bogusService.ScienceTrendingPodcasts;
    public List<TrendingPodcast> TrueCrimeTrendingPodcasts =>
        bogusService.TrueCrimeTrendingPodcasts;

    public Dictionary<string, List<Category>> GroupedCategories => bogusService.GroupedCategories;

    public List<string> GroupedCategoryNames => bogusService.GroupedCategoryNames;

    [RelayCommand]
    private void PodcastClick(object? commandParameter)
    {
        navigationService.Navigate(
            nameof(PodcastViewModel),
            commandParameter,
            new DrillInNavigationTransitionInfo()
        );
    }
}
