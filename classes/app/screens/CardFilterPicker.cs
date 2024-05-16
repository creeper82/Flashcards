namespace Flashcards;

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
            CLI.Screens.CardFilterPicker(newCardFilter);
            var handleResult = Logic.HandleCardFilterPicker(newCardFilter);

            if (handleResult == Logic.HandleCardFilterResult.ExitScreen) running = false;
            if (handleResult == Logic.HandleCardFilterResult.ApplyFilter)
            {
                return newCardFilter;
            }
        }

        return currentCardFilter;
    }
}