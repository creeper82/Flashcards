namespace Flashcards;

public static partial class App
{
    public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            CLI.Screens.Deck(deck);
            var handleDeckResult = Logic.HandleDeck(database, deck);
            if (handleDeckResult == Logic.HandleDeckResult.Exit) break;
            if (handleDeckResult == Logic.HandleDeckResult.OpenStudySession) StudySession(database, deck.Cards);
            if (handleDeckResult == Logic.HandleDeckResult.OpenDeckDetails) DeckDetails(deck);
            if (handleDeckResult == Logic.HandleDeckResult.OpenCardEditor) DeckCardList(database, deck);
        }

    }
}