using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using PlutoCast.Desktop.Models;

namespace PlutoCast.Desktop.Services;

public class BogusService
{
    private readonly List<TrendingPodcast> _trendingPodcasts = [];
    private readonly List<TrendingPodcast> _newTrendingPodcasts = [];
    private readonly List<TrendingPodcast> _trueCrimeTrendingPodcasts = [];
    private readonly List<TrendingPodcast> _comedyTrendingPodcasts = [];
    private readonly List<TrendingPodcast> _scienceTrendingPodcasts = [];
    private readonly List<Category> _categories =
        new()
        {
            new(1, "Arts"),
            new(2, "Books"),
            new(3, "Design"),
            new(4, "Fashion"),
            new(5, "Beauty"),
            new(6, "Food"),
            new(7, "Performing"),
            new(8, "Visual"),
            new(9, "Business"),
            new(10, "Careers"),
            new(11, "Entrepreneurship"),
            new(12, "Investing"),
            new(13, "Management"),
            new(14, "Marketing"),
            new(15, "Non-Profit"),
            new(16, "Comedy"),
            new(17, "Interviews"),
            new(18, "Improv"),
            new(19, "Stand-Up"),
            new(20, "Education"),
            new(21, "Courses"),
            new(22, "How-To"),
            new(23, "Language"),
            new(24, "Learning"),
            new(25, "Self-Improvement"),
            new(26, "Fiction"),
            new(27, "Drama"),
            new(28, "History"),
            new(29, "Health"),
            new(30, "Fitness"),
            new(31, "Alternative"),
            new(32, "Medicine"),
            new(33, "Mental"),
            new(34, "Nutrition"),
            new(35, "Sexuality"),
            new(36, "Kids"),
            new(37, "Family"),
            new(38, "Parenting"),
            new(39, "Pets"),
            new(40, "Animals"),
            new(41, "Stories"),
            new(42, "Leisure"),
            new(43, "Animation"),
            new(44, "Manga"),
            new(45, "Automotive"),
            new(46, "Aviation"),
            new(47, "Crafts"),
            new(48, "Games"),
            new(49, "Hobbies"),
            new(50, "Home"),
            new(51, "Garden"),
            new(52, "Video-Games"),
            new(53, "Music"),
            new(54, "Commentary"),
            new(55, "News"),
            new(56, "Daily"),
            new(57, "Entertainment"),
            new(58, "Government"),
            new(59, "Politics"),
            new(60, "Buddhism"),
            new(61, "Christianity"),
            new(62, "Hinduism"),
            new(63, "Islam"),
            new(64, "Judaism"),
            new(65, "Religion"),
            new(66, "Spirituality"),
            new(67, "Science"),
            new(68, "Astronomy"),
            new(69, "Chemistry"),
            new(70, "Earth"),
            new(71, "Life"),
            new(72, "Mathematics"),
            new(73, "Natural"),
            new(74, "Nature"),
            new(75, "Physics"),
            new(76, "Social"),
            new(77, "Society"),
            new(78, "Culture"),
            new(79, "Documentary"),
            new(80, "Personal"),
            new(81, "Journals"),
            new(82, "Philosophy"),
            new(83, "Places"),
            new(84, "Travel"),
            new(85, "Relationships"),
            new(86, "Sports"),
            new(87, "Baseball"),
            new(88, "Basketball"),
            new(89, "Cricket"),
            new(90, "Fantasy"),
            new(91, "Football"),
            new(92, "Golf"),
            new(93, "Hockey"),
            new(94, "Rugby"),
            new(95, "Running"),
            new(96, "Soccer"),
            new(97, "Swimming"),
            new(98, "Tennis"),
            new(99, "Volleyball"),
            new(100, "Wilderness"),
            new(101, "Wrestling"),
            new(102, "Technology"),
            new(103, "True Crime"),
            new(104, "TV"),
            new(105, "Film"),
            new(106, "After-Shows"),
            new(107, "Reviews"),
            new(108, "Climate"),
            new(109, "Weather"),
            new(110, "Tabletop"),
            new(111, "Role-Playing"),
            new(112, "Cryptocurrency"),
        };

    public const string ArtsKey = "Arts and Design";
    public const string BusinessKey = "Business and Finance";
    public const string ComedyKey = "Comedy";
    public const string EducationKey = "Education and Learning";
    public const string HealthKey = "Health and Wellness";
    public const string FamilyKey = "Family and Lifestyle";
    public const string EntertainmentKey = "Entertainment";
    public const string HobbyKey = "Hobbies and Leisure";
    public const string NewsKey = "News and Politics";
    public const string ReligionKey = "Religion and Spirituality";
    public const string ScienceKey = "Science and Nature";
    public const string SocietyKey = "Society and Culture";
    public const string SportKey = "Sport";
    public const string TechnologyKey = "Technology";
    public const string TrueCrimeKey = "TrueCrime";
    public const string TvKey = "TV and Film";
    public const string GamingKey = "Gaming";

    public List<string> GroupedCategoryNames =>
        [
            ArtsKey,
            BusinessKey,
            ComedyKey,
            EducationKey,
            HealthKey,
            FamilyKey,
            EntertainmentKey,
            HobbyKey,
            NewsKey,
            ReligionKey,
            ScienceKey,
            SocietyKey,
            SportKey,
            TechnologyKey,
            TrueCrimeKey,
            TvKey,
            GamingKey
        ];

    public Dictionary<string, List<Category>> GroupedCategories { get; } =
        new()
        {
            {
                ArtsKey,

                [
                    new(1, "Arts"),
                    new(2, "Books"),
                    new(3, "Design"),
                    new(4, "Fashion"),
                    new(5, "Beauty"),
                    new(7, "Performing"),
                    new(8, "Visual")
                ]
            },
            {
                BusinessKey,

                [
                    new(9, "Business"),
                    new(10, "Careers"),
                    new(11, "Entrepreneurship"),
                    new(12, "Investing"),
                    new(13, "Management"),
                    new(14, "Marketing"),
                    new(15, "Non-Profit")
                ]
            },
            {
                ComedyKey,
                [new(16, "Comedy"), new(17, "Interviews"), new(18, "Improv"), new(19, "Stand-Up")]
            },
            {
                EducationKey,

                [
                    new(20, "Education"),
                    new(21, "Courses"),
                    new(22, "How-To"),
                    new(23, "Language"),
                    new(24, "Learning"),
                    new(25, "Self-Improvement"),
                    new(28, "History")
                ]
            },
            {
                HealthKey,

                [
                    new(29, "Health"),
                    new(30, "Fitness"),
                    new(31, "Alternative"),
                    new(32, "Medicine"),
                    new(33, "Mental"),
                    new(34, "Nutrition"),
                    new(35, "Sexuality")
                ]
            },
            {
                FamilyKey,

                [
                    new(36, "Kids"),
                    new(37, "Family"),
                    new(38, "Parenting"),
                    new(39, "Pets"),
                    new(40, "Animals")
                ]
            },
            {
                EntertainmentKey,

                [
                    new(26, "Fiction"),
                    new(27, "Drama"),
                    new(41, "Stories"),
                    new(43, "Animation"),
                    new(44, "Manga"),
                    new(45, "Automotive"),
                    new(46, "Aviation"),
                    new(53, "Music")
                ]
            },
            {
                HobbyKey,

                [
                    new(6, "Food"),
                    new(42, "Leisure"),
                    new(47, "Crafts"),
                    new(49, "Hobbies"),
                    new(50, "Home"),
                    new(51, "Garden")
                ]
            },
            {
                NewsKey,

                [
                    new(54, "Commentary"),
                    new(55, "News"),
                    new(56, "Daily"),
                    new(57, "Entertainment"),
                    new(58, "Government"),
                    new(59, "Politics")
                ]
            },
            {
                ReligionKey,

                [
                    new(60, "Buddhism"),
                    new(61, "Christianity"),
                    new(62, "Hinduism"),
                    new(63, "Islam"),
                    new(64, "Judaism"),
                    new(65, "Religion"),
                    new(66, "Spirituality")
                ]
            },
            {
                ScienceKey,

                [
                    new(67, "Science"),
                    new(68, "Astronomy"),
                    new(69, "Chemistry"),
                    new(70, "Earth"),
                    new(71, "Life"),
                    new(72, "Mathematics"),
                    new(73, "Natural"),
                    new(74, "Nature"),
                    new(75, "Physics"),
                    new(108, "Climate"),
                    new(109, "Weather")
                ]
            },
            {
                SocietyKey,

                [
                    new(76, "Social"),
                    new(77, "Society"),
                    new(78, "Culture"),
                    new(79, "Documentary"),
                    new(80, "Personal"),
                    new(81, "Journals"),
                    new(82, "Philosophy"),
                    new(83, "Places"),
                    new(84, "Travel"),
                    new(85, "Relationship")
                ]
            },
            {
                SportKey,

                [
                    new(86, "Sports"),
                    new(87, "Baseball"),
                    new(88, "Basketball"),
                    new(89, "Cricket"),
                    new(90, "Fantasy"),
                    new(91, "Football"),
                    new(92, "Golf"),
                    new(93, "Hockey"),
                    new(94, "Rugby"),
                    new(95, "Running"),
                    new(96, "Soccer"),
                    new(97, "Swimming"),
                    new(98, "Tennis"),
                    new(99, "Volleyball"),
                    new(100, "Wilderness"),
                    new(101, "Wrestling")
                ]
            },
            { TechnologyKey, [new(102, "Technology"), new(112, "Cryptocurrency")] },
            { TrueCrimeKey, [new(103, "True Crime")] },
            {
                TvKey,
                [new(104, "TV"), new(105, "Film"), new(106, "After-Show"), new(107, "Reviews")]
            },
            {
                GamingKey,

                [
                    new(48, "Games"),
                    new(52, "Video-Games"),
                    new(110, "Tabletop"),
                    new(111, "Role-Playing")
                ]
            }
        };

    public List<TrendingPodcast> TopTrendingPodcasts
    {
        get
        {
            if (_trendingPodcasts.Count == 0)
            {
                GetTrendingPodcasts(_trendingPodcasts);
            }

            return _trendingPodcasts;
        }
    }

    public List<TrendingPodcast> NewsTrendingPodcasts
    {
        get
        {
            if (_newTrendingPodcasts.Count == 0)
            {
                GetTrendingPodcasts(_newTrendingPodcasts);
            }
            return _newTrendingPodcasts;
        }
    }

    public List<TrendingPodcast> ComedyTrendingPodcasts
    {
        get
        {
            if (_comedyTrendingPodcasts.Count == 0)
            {
                GetTrendingPodcasts(_comedyTrendingPodcasts);
            }
            return _comedyTrendingPodcasts;
        }
    }

    public List<TrendingPodcast> ScienceTrendingPodcasts
    {
        get
        {
            if (_scienceTrendingPodcasts.Count == 0)
            {
                GetTrendingPodcasts(_scienceTrendingPodcasts);
            }
            return _scienceTrendingPodcasts;
        }
    }

    public List<TrendingPodcast> TrueCrimeTrendingPodcasts
    {
        get
        {
            if (_trueCrimeTrendingPodcasts.Count == 0)
            {
                GetTrendingPodcasts(_trueCrimeTrendingPodcasts);
            }
            return _trueCrimeTrendingPodcasts;
        }
    }

    private void GetTrendingPodcasts(List<TrendingPodcast> trendingPodcasts)
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
            trendingPodcasts.Add(podcast);
        }

        foreach (var trendingPodcast in trendingPodcasts)
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
