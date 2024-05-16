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
            var handleSortPickerResult = Logic.HandleSortPicker();

            if (handleSortPickerResult == Logic.HandleSortPickerResult.Exit) running = false;
            if (handleSortPickerResult == Logic.HandleSortPickerResult.MoveForward) sortTypeChoiceList.MoveForward();
            if (handleSortPickerResult == Logic.HandleSortPickerResult.MoveBackward) sortTypeChoiceList.MoveBackward();
        }

        return (SortType)sortTypeChoiceList.SelectedIndex;
    }
}