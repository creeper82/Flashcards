namespace Flashcards;

public static partial class Filtering
{
    // Determines which part of the card should be matched to the keyword
    public enum KeywordMatchMode
    {
        Any, CardFront, CardBack
    }

    // Returns a friendly name for a KeywordMatchMode
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
        public string Keyword = "";
        public int? RecentDays = null;

        public CardFilter(string keyword = "", KeywordMatchMode matchMode = 0, int? recentDays = null)
        {
            MatchMode = matchMode;
            Keyword = keyword;
            RecentDays = recentDays;
        }

    }

    
}