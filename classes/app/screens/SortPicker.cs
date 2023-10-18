namespace FlashcardsApp;

using CLI;
using static Flashcards.Sorting;

public static partial class App
{
    public static SortType SortTypePicker(SortType currentSortType = 0)
    {
        var sortTypes = Enum.GetValues(typeof(SortType));
        var sortTypeNames = sortTypes.Cast<SortType>().Select(v => v.SortFriendlyName()).ToList();
        var sortTypeChoiceList = new ChoiceList<string>(sortTypeNames)
        {
            selectedIndex = (int)currentSortType
        };

        bool running = true;

        while (running)
        {
            Screens.SortTypePicker(sortTypeNames, sortTypeChoiceList.selectedIndex);
            running = Logic.HandleSortPicker(sortTypeChoiceList);
        }

        return (SortType)sortTypeChoiceList.selectedIndex;
    }
}