namespace Flashcards;
using static Sorting;

public static class SortExtensionMethods {
    public static IEnumerable<Card> SortBy(this IEnumerable<Card> cards, SortType sortType) {
        return sortType switch
        {
            SortType.DATE_ASCENDING => cards.OrderBy(card => card.CreationTimestamp),
            SortType.DATE_DESCENDING => cards.OrderByDescending(card => card.CreationTimestamp),
            _ => cards
        };
    }
}
