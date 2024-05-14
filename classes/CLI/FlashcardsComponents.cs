namespace Flashcards.CLI;
using static SharpViews.Components;

public static class Components
{

    internal static string DeckCard(Card card, bool revealed = false)
    {
        int cardWidth = Math.Max(card.Front.Length, card.Back.Length);

        if (!revealed)
        {
            return CenteredText(card.Front);
        }
        else
        {
            return (
                CenteredWrappedText(card.Front) +
                HorizontalLine('-', Math.Min(cardWidth + 4, UiWidth)) +
                CenteredWrappedText(card.Back)
            );
        }
    }

    internal static string DeckList(IEnumerable<Deck> decks, int? selectedDeckIndex = null, int startIndex = 0)
    {
        var deckNames = decks.Select(deck => deck.Name);
        return List(deckNames, selectedDeckIndex, startIndex);
    }
}