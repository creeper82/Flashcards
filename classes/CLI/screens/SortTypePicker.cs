using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void SortTypePicker(List<string> sortTypes, int selectedIndex)
    {
        ClearConsole();
        Console.WriteLine(
            UiFrame(
                inner: List(sortTypes, selectedIndex),
                title: "Sort by",
                verticalScroll: true
            )
        );

        Console.WriteLine(KeyboardActionList(KeyboardActions.SortPickerScreen));
    }
}