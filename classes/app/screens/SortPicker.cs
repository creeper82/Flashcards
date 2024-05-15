namespace Flashcards;

using SharpViews;
using static Flashcards.Sorting;

public static partial class App
{
    public static SortType SortTypePicker(SortType currentSortType = 0)
    {
        var sortTypes = Enum.GetValues(typeof(SortType));
        var sortTypeNames = sortTypes.Cast<SortType>().Select(v => v.GetName()).ToList();
        var sortTypeChoiceList = new ChoiceList<string>(sortTypeNames, (int)currentSortType);

        bool running = true;

        while (running)
        {
            CLI.Screens.SortTypePicker(sortTypeNames, sortTypeChoiceList.SelectedIndex);
            running = Logic.HandleSortPicker(sortTypeChoiceList);
        }

        return (SortType)sortTypeChoiceList.SelectedIndex;
    }
}