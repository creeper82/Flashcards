namespace FlashcardsApp;

using CLI;
using static Flashcards.Sorting;

public static partial class App
{
    public static SortType SortTypePicker()
    {
        var sortTypes = Enum.GetValues(typeof(SortType));
        var sortTypeNames = sortTypes.Cast<SortType>().Select(v => v.SortFriendlyName());
        var sortTypeChoiceList = new ChoiceList<string>(sortTypeNames);
        bool running = true;

        while (running)
        {
            Screens.SortTypePicker(sortTypeNames.ToList(), sortTypeChoiceList.selectedIndex);
            running = Interactions.HandleSortPicker(sortTypeChoiceList);
        }

        return (SortType)sortTypeChoiceList.selectedIndex;
    }
}