using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Collections;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using PlutoCast.Desktop.Controls;
using PlutoCast.Desktop.Models;
using PlutoCast.Desktop.Services;

namespace PlutoCast.Desktop.ViewModels;

[ObservableObject]
public partial class PodcastViewModel(BogusService bogusService) : BaseViewModel
{
    [ObservableProperty]
    private TrendingPodcast? _podcast;

    [ObservableProperty]
    private string _totalDuration = string.Empty;

    [ObservableProperty]
    private string _podcastMetaDataText = string.Empty;

    [ObservableProperty]
    private string _episodeInfo = string.Empty;

    public AdvancedCollectionView Episodes { get; } = [];

    public void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not TrendingPodcast podcast)
            return;

        GetEpisodes(podcast.Id);
        PodcastMetaDataText = GetFriendlyPodcastInfo(podcast);
        Podcast = podcast;
    }

    [RelayCommand]
    private async Task GetDetails(TrendingPodcast podcast)
    {
        PodcastDetailsContentDialog dialog =
            new()
            {
                Podcast = podcast,
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.None,
            };

        var result = await dialog.ShowAsync();
    }

    private void GetEpisodes(long id)
    {
        foreach (var episode in bogusService.GetEpisodes(id))
        {
            Episodes.Add(episode);
        }

        Episodes.Refresh();
    }

    private static string GetFriendlyPodcastInfo(TrendingPodcast podcast)
    {
        var author = podcast.Author;
        var episodeCount = podcast.Episodes.Count;
        var totalDuration = podcast.GetFriendlyDuration();

        return episodeCount > 0
            ? $"{author} • {episodeCount} Episodes • {totalDuration}"
            : $"{author}";
    }

    public void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
        {
            return;
        }

        if (Episodes.Any())
        {
            Episodes.Filter = Filter;
        }

        return;

        bool Filter(object o)
        {
            if (o is not Episode episode)
            {
                return false;
            }
            return episode.Title is not null
                && episode.Title.Contains(sender.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
