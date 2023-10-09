namespace CLI;

public class KeyboardAction
{
    public string key = "";
    public string optionText = "";

    public KeyboardAction(string key, string optionText)
    {
        this.key = key;
        this.optionText = optionText;
    }

    public override string ToString()
    {
        // Empty option string is used as a line separator
        return (key == "" && optionText == "") ? "" : $"[ {key} ] - {optionText}";
    }

    public static KeyboardAction LineSeparator => new("", "");
}
// List of commonly used keyboard actions in screens
public static class KeyboardActions
{
    public static List<KeyboardAction> DeckListScreen { get; } = new() {
        new("up/down", "move selection"),
        new("enter", "open deck"),
        new("del", "delete deck"),
        new("r", "rename deck"),
        KeyboardAction.LineSeparator,
        new("n", "create new deck")
    };

    public static List<KeyboardAction> DeckListScreenEmpty { get; } = new() {
        new("up/down", "move selection"),
        new("n", "create new deck")
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
}