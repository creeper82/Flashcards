namespace Flashcards;
using SharpViews;

public static partial class App
{
    public static void Menu(FlashcardsDatabase database)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ChoiceList<Deck> deckChoiceList = new(database.GetDecks())
        {
            PaginationCount = 5
        };

        bool running = true;

        while (running)
        {
            CLI.Screens.Menu(deckChoiceList.PaginatedChoices, deckChoiceList.SelectedIndex, deckChoiceList.PaginationStartIndex);

            var handleMenuResult = Logic.HandleMenu(database, deckChoiceList);

            if (handleMenuResult == Logic.HandleMenuResult.Exit) break;
            if (handleMenuResult == Logic.HandleMenuResult.MoveForward) deckChoiceList.MoveForward();
            if (handleMenuResult == Logic.HandleMenuResult.MoveBackward) deckChoiceList.MoveBackward();
            if (handleMenuResult == Logic.HandleMenuResult.OpenDeck && deckChoiceList.SelectedChoice is not null) Deck(database, deckChoiceList.SelectedChoice);
            if (handleMenuResult == Logic.HandleMenuResult.OpenHelp) Help(database.Path);
        }

    }
}