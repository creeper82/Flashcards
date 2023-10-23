using Flashcards;
namespace CLI;

using static Components;
using static Flashcards.Filtering;

public partial class Screens
{
    internal static void CardFilter(CardFilter cardFilter)
    {
        ClearConsole();

        bool keywordFilterApplied = cardFilter.Keyword is not null;
        bool daysFilterApplied = cardFilter.RecentDays is not null;
        bool noFiltersApplied = !keywordFilterApplied && !daysFilterApplied;


        string filterText = (
            noFiltersApplied
            ? "No filters applied"
            : "Filters applied:"
        ) + "\n" + (
            keywordFilterApplied
            ? $"{cardFilter.MatchMode.FriendlyName()}: '{cardFilter.Keyword}'"
            : ""
        ) + "\n" + (
            daysFilterApplied
            ? $"Only cards added last {cardFilter.RecentDays} days"
            : ""
        );

        Console.WriteLine(
            UiFrame(
                CenteredText(filterText),
                "Filter by"
            )
        );
    }
}