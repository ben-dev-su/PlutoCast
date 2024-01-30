using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using PlutoCast.Desktop.Models;

namespace PlutoCast.Desktop.Services;

public class BogusService
{
    private readonly List<TrendingPodcast> _trendingPodcasts = [];

    private readonly List<Category> _categories =
        new()
        {
            new Category(1, "Arts"),
            new Category(2, "Books"),
            new Category(3, "Design"),
            new Category(4, "Fashion"),
            new Category(5, "Beauty"),
            new Category(6, "Food"),
            new Category(7, "Performing"),
            new Category(8, "Visual"),
            new Category(9, "Business"),
            new Category(10, "Careers"),
            new Category(11, "Entrepreneurship"),
            new Category(12, "Investing"),
            new Category(13, "Management"),
            new Category(14, "Marketing"),
            new Category(15, "Non-Profit"),
            new Category(16, "Comedy"),
            new Category(17, "Interviews"),
            new Category(18, "Improv"),
            new Category(19, "Stand-Up"),
            new Category(20, "Education"),
            new Category(21, "Courses"),
            new Category(22, "How-To"),
            new Category(23, "Language"),
            new Category(24, "Learning"),
            new Category(25, "Self-Improvement"),
            new Category(26, "Fiction"),
            new Category(27, "Drama"),
            new Category(28, "History"),
            new Category(29, "Health"),
            new Category(30, "Fitness"),
            new Category(31, "Alternative"),
            new Category(32, "Medicine"),
            new Category(33, "Mental"),
            new Category(34, "Nutrition"),
            new Category(35, "Sexuality"),
            new Category(36, "Kids"),
            new Category(37, "Family"),
            new Category(38, "Parenting"),
            new Category(39, "Pets"),
            new Category(40, "Animals"),
            new Category(41, "Stories"),
            new Category(42, "Leisure"),
            new Category(43, "Animation"),
            new Category(44, "Manga"),
            new Category(45, "Automotive"),
            new Category(46, "Aviation"),
            new Category(47, "Crafts"),
            new Category(48, "Games"),
            new Category(49, "Hobbies"),
            new Category(50, "Home"),
            new Category(51, "Garden"),
            new Category(52, "Video-Games"),
            new Category(53, "Music"),
            new Category(54, "Commentary"),
            new Category(55, "News"),
            new Category(56, "Daily"),
            new Category(57, "Entertainment"),
            new Category(58, "Government"),
            new Category(59, "Politics"),
            new Category(60, "Buddhism"),
            new Category(61, "Christianity"),
            new Category(62, "Hinduism"),
            new Category(63, "Islam"),
            new Category(64, "Judaism"),
            new Category(65, "Religion"),
            new Category(66, "Spirituality"),
            new Category(67, "Science"),
            new Category(68, "Astronomy"),
            new Category(69, "Chemistry"),
            new Category(70, "Earth"),
            new Category(71, "Life"),
            new Category(72, "Mathematics"),
            new Category(73, "Natural"),
            new Category(74, "Nature"),
            new Category(75, "Physics"),
            new Category(76, "Social"),
            new Category(77, "Society"),
            new Category(78, "Culture"),
            new Category(79, "Documentary"),
            new Category(80, "Personal"),
            new Category(81, "Journals"),
            new Category(82, "Philosophy"),
            new Category(83, "Places"),
            new Category(84, "Travel"),
            new Category(85, "Relationships"),
            new Category(86, "Sports"),
            new Category(87, "Baseball"),
            new Category(88, "Basketball"),
            new Category(89, "Cricket"),
            new Category(90, "Fantasy"),
            new Category(91, "Football"),
            new Category(92, "Golf"),
            new Category(93, "Hockey"),
            new Category(94, "Rugby"),
            new Category(95, "Running"),
            new Category(96, "Soccer"),
            new Category(97, "Swimming"),
            new Category(98, "Tennis"),
            new Category(99, "Volleyball"),
            new Category(100, "Wilderness"),
            new Category(101, "Wrestling"),
            new Category(102, "Technology"),
            new Category(103, "True Crime"),
            new Category(104, "TV"),
            new Category(105, "Film"),
            new Category(106, "After-Shows"),
            new Category(107, "Reviews"),
            new Category(108, "Climate"),
            new Category(109, "Weather"),
            new Category(110, "Tabletop"),
            new Category(111, "Role-Playing"),
            new Category(112, "Cryptocurrency"),
        };

    public List<TrendingPodcast> TrendingPodcasts
    {
        get
        {
            if (!_trendingPodcasts.Any())
            {
                GetTrendingPodcasts();
            }

            return _trendingPodcasts;
        }
    }

    private void GetTrendingPodcasts()
    {
        foreach (
            var podcast in new Faker<TrendingPodcast>()
                .RuleFor(p => p.Id, f => f.Random.Long(1000, 100000))
                .RuleFor(p => p.Url, f => new Uri(f.Internet.Url()))
                .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Sentences(f.Random.Int(2, 8)))
                .RuleFor(p => p.Author, f => f.Company.CompanyName())
                .RuleFor(p => p.Image, f => new Uri(f.Image.PicsumUrl()))
                .RuleFor(p => p.Artwork, f => new Uri(f.Image.PicsumUrl(2560, 1440)))
                .RuleFor(p => p.NewestItemPublishedTime, f => f.Date.PastOffset())
                .RuleFor(p => p.ItunesId, f => f.Random.Long(1000, 100000))
                .RuleFor(p => p.TrendScore, f => f.Random.Int(1, 20))
                .RuleFor(p => p.Language, f => "en")
                .Generate(10)
                .ToList()
        )
        {
            _trendingPodcasts.Add(podcast);
        }

        foreach (var trendingPodcast in _trendingPodcasts)
        {
            foreach (
                var category in _categories
                    .OrderBy(x => Guid.NewGuid())
                    .Take(Random.Shared.Next(1, 10))
                    .ToHashSet()
            )
            {
                trendingPodcast.AddCategory(category);
            }
        }
    }
}
