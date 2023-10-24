namespace FlashcardsApp;

using CLI;
using Flashcards;

using static Flashcards.Filtering;

public static partial class App
{
    public static CardFilter CardFilterPicker(CardFilter? currentCardFilter = null)
    {
        bool running = true;
        
        currentCardFilter ??= new();
        CardFilter newCardFilter = currentCardFilter.Clone();
        
        while (running)
        {
            Screens.CardFilterPicker(newCardFilter);
            var handleResult = Logic.HandleCardFilterPicker(newCardFilter);

            if (handleResult is Logic.HandleCardFilterResult.ExitScreen) running = false;
            if (handleResult is Logic.HandleCardFilterResult.ApplyFilter) {
                return newCardFilter;
            }
        }

        return currentCardFilter;
    }
}