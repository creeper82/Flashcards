namespace Flashcards;

public static partial class Filtering
{
    public static IEnumerable<Card> ApplyFilter(this IEnumerable<Card> cards, CardFilter cardFilter)
    {
        IEnumerable<Card> filteredCards = cards;

        // filter the cards by keyword
        if (cardFilter.HasKeywordFilter)
        {
            filteredCards = filteredCards.Where(card =>
        {
            string keyword = cardFilter.Keyword.ToLower();
            string front = card.Front.ToLower();
            string back = card.Back.ToLower();

            return cardFilter.MatchMode switch
            {
                KeywordMatchMode.Any => front.Contains(keyword) || back.Contains(keyword),
                KeywordMatchMode.CardFront => front.Contains(keyword),
                KeywordMatchMode.CardBack => back.Contains(keyword),
                _ => true,
            };
        });
        }

        // filter the cards by date
        if (cardFilter.HasDaysFilter)
        {
            filteredCards = filteredCards.Where(card => (DateTime.UtcNow - card.CreationTimestamp).Days <= cardFilter.RecentDays);
        }

        // filter the cards by tagged state
        if (cardFilter.HasTaggedFilter)
        {
            filteredCards = filteredCards.Where(card => card.Tagged);
        }

        return filteredCards;
    }
}