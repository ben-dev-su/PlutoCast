using System;
using System.Collections.Generic;
using System.Linq;

namespace PlutoCast.Desktop.Models;

public class TrendingPodcast
{
    private readonly List<Category> _categories = [];
    private readonly List<Episode> _episodes = [];
    public long Id { get; init; }
    public Uri? Url { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? Author { get; init; }
    public Uri? Image { get; init; }
    public Uri? Artwork { get; init; }
    public DateTimeOffset NewestItemPublishedTime { get; init; }
    public long ItunesId { get; init; }
    public int TrendScore { get; init; }
    public string? Language { get; init; }
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyList<Episode> Episodes => _episodes.AsReadOnly();

    public string GetFriendlyDuration()
    {
        if (Episodes.Count == 0)
        {
            return string.Empty;
        }
        var totalDuration = Episodes.Sum(x => x.Duration);
        if (totalDuration < 60)
        {
            return $"{totalDuration} min";
        }
        var hours = totalDuration / 60;
        var minutes = totalDuration % 60;
        return $"{hours} hr {(minutes > 0 ? $"{minutes} min" : string.Empty)}";
    }

    public void AddEpisode(Episode episode)
    {
        _episodes.Add(episode);
    }

    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }
}
