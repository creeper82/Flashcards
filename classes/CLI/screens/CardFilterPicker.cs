using Flashcards;
namespace CLI;

using static Components;
using static Flashcards.Filtering;

public partial class Screens
{
    internal static void CardFilterPicker(CardFilter cardFilter)
    {
        ClearConsole();


        string filterText = (
            !cardFilter.HasAnyFilter
            ? "No filters applied"
            : "Filters applied:"
        ) + "\n\n" + (
            cardFilter.HasKeywordFilter
            ? $"{cardFilter.MatchMode.FriendlyName()}: '{cardFilter.Keyword}'\n"
            : ""
        ) + (
            cardFilter.HasDaysFilter
            ? $"Only cards added last {cardFilter.RecentDays} days\n"
            : ""
        ) + (
            cardFilter.HasTaggedFilter
            ? $"Only cards tagged \"still learning\"\n"
            : ""
        );

        Console.WriteLine(
            UiFrame(
                CenteredText(filterText),
                "Filter by"
            )
        );

        Console.WriteLine(
            KeyboardActionList(KeyboardActions.CardFilterPickerScreen)
        );
    }
}