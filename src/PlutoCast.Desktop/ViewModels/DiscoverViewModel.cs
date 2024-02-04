using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PlutoCast.Desktop.Models;
using PlutoCast.Desktop.Services;

namespace PlutoCast.Desktop.ViewModels;

[ObservableObject]
public partial class DiscoverViewModel : BaseViewModel
{
    private readonly BogusService _bogusService;

    public DiscoverViewModel(BogusService bogusService)
    {
        _bogusService = bogusService;
    }

    public List<TrendingPodcast> TopTrendingPodcasts => _bogusService.TopTrendingPodcasts;
    public List<TrendingPodcast> NewsTrendingPodcasts => _bogusService.NewsTrendingPodcasts;
    public List<TrendingPodcast> ComedyTrendingPodcasts => _bogusService.ComedyTrendingPodcasts;
    public List<TrendingPodcast> ScienceTrendingPodcasts => _bogusService.ScienceTrendingPodcasts;
    public List<TrendingPodcast> TrueCrimeTrendingPodcasts =>
        _bogusService.TrueCrimeTrendingPodcasts;

    public Dictionary<string, List<Category>> GroupedCategories => _bogusService.GroupedCategories;

    public List<string> GroupedCategoryNames => _bogusService.GroupedCategoryNames;
}
