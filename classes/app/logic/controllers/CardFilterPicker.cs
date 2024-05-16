namespace Flashcards;

using SharpViews;
using static Flashcards.Filtering;

public static partial class Logic
{
    public enum HandleCardFilterResult {
        ContinueLoop, ApplyFilter, ExitScreen
    }

    public static HandleCardFilterResult HandleCardFilterPicker(CardFilter cardFilter)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        switch (consoleKey)
        {
            case ConsoleKey.D1:
                UpdateKeywordFilter(cardFilter, KeywordMatchMode.Any);
                break;
            case ConsoleKey.D2:
                UpdateKeywordFilter(cardFilter, KeywordMatchMode.CardFront);
                break;
            case ConsoleKey.D3:
                UpdateKeywordFilter(cardFilter, KeywordMatchMode.CardBack);
                break;
            case ConsoleKey.D4:
                UpdateRecentDaysFilter(cardFilter);
                break;
            case ConsoleKey.D5:
                UpdateTaggedFilter(cardFilter);
                break;
            case ConsoleKey.C:
                ResetCardFilter(cardFilter);
                break;
            case ConsoleKey.Enter:
                return HandleCardFilterResult.ApplyFilter;
            case ConsoleKey.Escape:
                return HandleCardFilterResult.ExitScreen;
        }

        return HandleCardFilterResult.ContinueLoop;
    }
}