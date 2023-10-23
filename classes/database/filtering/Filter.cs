namespace Flashcards;

public static class Filtering
{
    // Determines which part of the card should be matched to the keyword
    public enum KeywordMatchMode
    {
        Any, CardFront, CardBack
    }

    public static string FriendlyName(this KeywordMatchMode matchMode) {
        return matchMode switch
        {
            KeywordMatchMode.Any => "Search the whole card",
            KeywordMatchMode.CardFront => "Search the card front",
            KeywordMatchMode.CardBack => "Search the card back",
            _ => "Search",
        };
    }

    public class CardFilter
    {

        public KeywordMatchMode MatchMode = KeywordMatchMode.Any;
        public string? Keyword = null;
        public int? RecentDays = null;

        public CardFilter(string? keyword = null, KeywordMatchMode matchMode = 0, int? recentDays = null)
        {
            MatchMode = matchMode;
            Keyword = keyword;
            RecentDays = recentDays;
        }

    }

    public static IEnumerable<Card> ApplyFilter(this IEnumerable<Card> cards, CardFilter cardFilter)
    {

        IEnumerable<Card> filteredCards = cards;

        // filter cards by keyword
        if (cardFilter.Keyword is not null)
        {
            filteredCards = cards.Where(card =>
        {
            string keyword = cardFilter.Keyword;
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

        // filter cards by date
        if (cardFilter.RecentDays is not null)
        {
            filteredCards = cards.Where(card => (DateTime.UtcNow - card.CreationTimestamp).Days <= cardFilter.RecentDays);
        }

        return filteredCards;
    }
}