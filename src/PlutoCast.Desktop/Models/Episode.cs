using System;
using PlutoCast.Desktop.Enums;

namespace PlutoCast.Desktop.Models;

public class Episode
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public Uri? Link { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset DatePublished { get; set; }
    public string? DatePublishedPretty { get; set; }
    public DateTimeOffset DateCrawled { get; set; }
    public Uri? EnclosureUrl { get; set; }
    public long EnclosureLength { get; set; }
    public long Duration { get; set; }
    public bool Explicit { get; set; }
    public int EpisodeNumber { get; set; }
    public EpisodeType EpisodeType { get; set; }
    public int Season { get; set; }
    public Uri? Image { get; set; }
    public long FeedItunesId { get; set; }
    public Uri? FeedImage { get; set; }
    public long FeedId { get; set; }
    public string? FeedLanguage { get; set; }
    public bool FeedDead { get; set; }
    public Uri? TranscriptUrl { get; set; }
    public string? FeedAuthor { get; set; }

    public string EpisodeInfo => GetFriendlyEpisodeInfo();

    private string GetFriendlyEpisodeInfo()
    {
        const string separator = " • ";

        var datePublished = string.IsNullOrEmpty(DatePublishedPretty)
            ? string.Empty
            : $"{DatePublishedPretty}{separator}";

        var season = Season > 0 ? $"{Season} Season{separator}" : string.Empty;
        var episode = EpisodeNumber > 0 ? $"{EpisodeNumber} Episode{separator}" : string.Empty;
        var duration = Duration > 0 ? $"{Duration} Min" : string.Empty;

        return $"{datePublished}{season}{episode}{duration}";
    }
}
