using System;
using System.Collections.Generic;

namespace PlutoCast.Desktop.Models;

public class TrendingPodcast
{
    private readonly List<Category> _categories = [];
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();
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

    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }
}