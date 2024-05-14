namespace Flashcards;

public static class Sorting
{
    public enum SortType
    {
        DATE_DESCENDING,
        DATE_ASCENDING,
        FRONT_ASCENDING,
        FRONT_DESCENDING,
        BACK_ASCENDING,
        BACK_DESCENDING
    }

    public static string SortFriendlyName(this SortType sortType)
    {
        return sortType switch
        {
            SortType.DATE_DESCENDING => "Newest first",
            SortType.DATE_ASCENDING => "Oldest first",
            SortType.FRONT_ASCENDING => "A-Z card front",
            SortType.FRONT_DESCENDING => "Z-A card front",
            SortType.BACK_ASCENDING => "A-Z card back",
            SortType.BACK_DESCENDING => "Z-A card back",
            _ => "Unknown sort",
        };
    }
}

