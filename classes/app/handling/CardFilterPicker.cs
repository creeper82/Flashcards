namespace FlashcardsApp;

using Flashcards;
using static Flashcards.Filtering;

public static partial class Logic
{
    // custom return status type
    public class HandleCardFilterResult {
        public class ContinueLoop : HandleCardFilterResult { }
        public class ApplyFilter : HandleCardFilterResult { }
        public class ExitScreen : HandleCardFilterResult { }
    }
    public static HandleCardFilterResult HandleCardFilterPicker(CardFilter cardFilter)
    {
        ConsoleKey consoleKey = Console.ReadKey().Key;

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
                return new HandleCardFilterResult.ApplyFilter();
            case ConsoleKey.Escape:
                return new HandleCardFilterResult.ExitScreen();
        }

        return new HandleCardFilterResult.ContinueLoop();
    }
}