namespace Flashcards;

public static partial class Filtering
{
    public static IEnumerable<Card> ApplyFilter(this IEnumerable<Card> cards, CardFilter cardFilter)
    {
        IEnumerable<Card> filteredCards = cards;

        // filter the cards by keyword
        if (cardFilter.Keyword != "")
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
        if (cardFilter.RecentDays is not null)
        {
            filteredCards = filteredCards.Where(card => (DateTime.UtcNow - card.CreationTimestamp).Days <= cardFilter.RecentDays);
        }

        return filteredCards;
    }
}