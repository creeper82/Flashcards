namespace FlashcardsApp;

using Flashcards;
using static Flashcards.Filtering;

public static partial class Logic
{
    private static void UpdateKeywordFilter(CardFilter cardFilter, KeywordMatchMode desiredMatchMode)
    {
        string newValue = CLI.Dialogs.Input(
            title: "Filter by",
            message: desiredMatchMode.FriendlyName(),
            bottomNote: "Leave empty to disable this filter"
        ).TrimStart();

        if (newValue != "")
        {
            // update the keyword filtering according to user's response
            cardFilter.MatchMode = desiredMatchMode;
            cardFilter.Keyword = newValue;
        }
        else
        {
            // when nothing inputted, disable the keyword filtering
            cardFilter.MatchMode = KeywordMatchMode.Any;
            cardFilter.Keyword = "";
        }

    }

    private static void UpdateRecentDaysFilter(CardFilter cardFilter)
    {
        string newDaysValue = CLI.Dialogs.Input(
            title: "Filter by",
            message: "How much recent days to show the cards from?",
            bottomNote: "Leave empty to disable this filter"
        ).Trim();

        if (newDaysValue != "")
        {
            try { cardFilter.RecentDays = int.Parse(newDaysValue); }
            catch { return; }
        }
        else cardFilter.RecentDays = null;
    }

    private static void UpdateTaggedFilter(CardFilter cardFilter) {
        cardFilter.OnlyTagged = !cardFilter.OnlyTagged;
    }

    private static void ResetCardFilter(CardFilter cardFilter)
    {
        if (CLI.Dialogs.Confirm(
            title: "Reset filter",
            message: "Do you want to reset all the applied filters?",
            okButton: "reset",
            cancelButton: "cancel"
        )) cardFilter.ResetFilter();
    }

}