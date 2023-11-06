namespace Flashcards;
using static Sorting;

public static class SortExtensionMethods
{
    public static List<Card> Shuffle(this IEnumerable<Card> cards)
    {
        Random rng = new();
        return cards.OrderBy(card => rng.Next()).ToList();
    }
    public static IEnumerable<Card> SortBy(this IEnumerable<Card> cards, SortType sortType)
    {
        return sortType switch
        {
            SortType.DATE_ASCENDING => cards.OrderBy(card => card.CreationTimestamp),
            SortType.DATE_DESCENDING => cards.OrderByDescending(card => card.CreationTimestamp),
            SortType.FRONT_ASCENDING => cards.OrderBy(card => card.Front),
            SortType.FRONT_DESCENDING => cards.OrderByDescending(card => card.Front),
            SortType.BACK_ASCENDING => cards.OrderBy(card => card.Back),
            SortType.BACK_DESCENDING => cards.OrderByDescending(card => card.Back),
            _ => cards
        };
    }
}
