namespace Flashcards;

public static class Sorting
{
    public enum SortType
    {
        DATE_ASCENDING,
        DATE_DESCENDING
    }

    public static string SortFriendlyName(this SortType sortType) {
        return sortType switch
        {
            SortType.DATE_ASCENDING => "oldest first",
            SortType.DATE_DESCENDING => "newest first",
            _ => "unknown sort type",
        };
    }

}

