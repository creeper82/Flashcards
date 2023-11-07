namespace Flashcards;

public static partial class Filtering
{
    // Determines which part of the card should be matched to the keyword
    public enum KeywordMatchMode
    {
        Any, CardFront, CardBack
    }

    // Returns a friendly name for a KeywordMatchMode
    public static string FriendlyName(this KeywordMatchMode matchMode)
    {
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

        public string Keyword = "";
        public KeywordMatchMode MatchMode = KeywordMatchMode.Any;
        public int? RecentDays = null;
        public bool OnlyTagged = false;

        public CardFilter(string keyword = "", KeywordMatchMode matchMode = 0, int? recentDays = null, bool onlyTagged = false)
        {
            MatchMode = matchMode;
            Keyword = keyword;
            RecentDays = recentDays;
            OnlyTagged = onlyTagged;
        }

        public void ResetFilter()
        {
            MatchMode = KeywordMatchMode.Any;
            Keyword = "";
            RecentDays = null;
            OnlyTagged = false;
        }

        public CardFilter Clone() {
            return new CardFilter (Keyword, MatchMode, RecentDays, OnlyTagged);
        }

        public bool HasKeywordFilter {
            get => Keyword != "";
        }

        public bool HasDaysFilter {
            get => RecentDays is not null;
        }

        public bool HasAnyFilter {
            get => HasKeywordFilter || HasDaysFilter || HasTaggedFilter;
        }

        public bool HasTaggedFilter {
            get => OnlyTagged;
        }

    }


}