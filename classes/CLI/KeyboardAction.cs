namespace CLI;

// KeyboardAction represents a key you have to press and a name of action it'll trigger
public class KeyboardAction
{
    public string Key = "";
    public string OptionText = "";

    public KeyboardAction(string key, string optionText)
    {
        Key = key;
        OptionText = optionText;
    }

    public override string ToString()
    {
        // Empty option string is used as a line separator
        return (Key == "" && OptionText == "") ? "" : $"[ {Key} ] - {OptionText}";
    }

    public static KeyboardAction LineSeparator => new("", "");
}

// List of commonly used keyboard actions among app screens
public static class KeyboardActions
{
    public static List<KeyboardAction> DeckListScreen { get; } = new() {
        new("up/down", "move selection"),
        new("enter", "open deck"),
        new("del", "delete deck"),
        new("r", "rename deck"),
        KeyboardAction.LineSeparator,
        new("n", "create new deck"),
        new("h", "open help")
    };

    public static List<KeyboardAction> DeckListScreenEmpty { get; } = new() {
        new("up/down", "move selection"),
        new("n", "create new deck"),
        new("h", "open help")
    };

    public static List<KeyboardAction> DeckScreen { get; } = new() {
        new("enter", "study cards"),
        new("del", "delete deck"),
        new("r", "rename deck"),
        new("c", "cards in deck"),
        new("i", "deck details"),
        KeyboardAction.LineSeparator,
        new("esc", "go back")
    };

    public static List<KeyboardAction> DeckScreenEmpty { get; } = new() {
        new("del", "delete deck"),
        new("r", "rename deck"),
        new("c", "cards in deck"),
        new("i", "deck details"),
        KeyboardAction.LineSeparator,
        new("esc", "go back")
    };

    public static List<KeyboardAction> DeckCardListScreen { get; } = new() {
        new("left/right", "move selection"),
        new("enter", "edit card"),
        new("del", "delete card"),
        new("t", "tag/untag for repetition"),
        KeyboardAction.LineSeparator,
        new("s", "change sort order"),
        new("f", "filter cards (search etc)"),
        KeyboardAction.LineSeparator,
        new("n", "create new card"),
        new("esc", "go back")
    };

    public static List<KeyboardAction> DeckCardListScreenEmpty { get; } = new() {
        new("n", "create new card"),
        new("esc", "go back")
    };

    public static List<KeyboardAction> DeckCardListScreenEmptyFiltered { get; } = new() {
        new("n", "create new card"),
        new("f", "filter cards (search etc)"),
        new("esc", "go back")
    };

    public static List<KeyboardAction> CardEditorScreen { get; } = new() {
        new("up", "edit card front"),
        new("down", "edit card back"),
        new("s", "swap front and back"),
        KeyboardAction.LineSeparator,
        new("enter", "save changes"),
        new("esc", "discard changes")
    };

    public static List<KeyboardAction> SortPickerScreen { get; } = new() {
        new("up/down", "move selection"),
        new("enter", "apply sort")
    };

    public static List<KeyboardAction> StudySessionScreen { get; } = new() {
        new("enter", "reveal / next card"),
        new("left", "previous card"),
        new("t", "tag/untag for repetition"),
        KeyboardAction.LineSeparator,
        new("c", "study only tagged cards"),
        new("r", "restart study session"),
        new("esc", "go back")
    };

    public static List<KeyboardAction> StudySessionScreenEmpty { get; } = new() {
        new("esc", "go back")
    };

    public static List<KeyboardAction> CardFilterPickerScreen { get; } = new() {
        new("1", "search the whole card"),
        new("2", "search the card fronts"),
        new("3", "search the card backs"),
        new("4", "show only cards added last X days"),
        new("5", "show only tagged cards"),
        new("C", "clear all filters"),
        KeyboardAction.LineSeparator,
        new("enter", "apply filter"),
        new("esc", "cancel")
    };
}