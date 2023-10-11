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

    public static List<KeyboardAction> CardEditorScreen { get; } = new() {
        new("left/right", "move selection"),
        new("enter", "edit card"),
        new("del", "delete card"),
        KeyboardAction.LineSeparator,
        new("n", "create new card"),
        new("esc", "go back")
    };

    public static List<KeyboardAction> CardEditorScreenEmpty { get; } = new() {
        new("n", "create new card"),
        new("esc", "go back")
    };
}