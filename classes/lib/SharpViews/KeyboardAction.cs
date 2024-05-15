namespace SharpViews;

/// <summary>
/// KeyboardAction represents a possible keyboard interaction (key, and the action name) for the current screen.
/// </summary>
/// <param name="key">Key to trigger the action, e.g. <c>Space</c></param>
/// <param name="actionName">Name of the action, e.g. <c>Open selected article</c></param>
/// <remarks>To display a list of keyboard actions, use <c>Components.KeyboardActionList</c>. It's best to create a class
/// with lists of keyboard actions for each screen. Tip: <c>KeyboardAction.LineSeparator</c> acts as an empty line to separate actions in a list.</remarks>
public class KeyboardAction(string key, string actionName)
{
    public string Key = key;
    public string OptionText = actionName;

    public override string ToString()
    {
        // Empty option string is used as a line separator
        return (Key == "" && OptionText == "") ? "" : $"[ {Key} ] - {OptionText}";
    }

    /// <summary>
    /// Empty keyboard action. When displayed with <c>Components.KeyboardActionList</c>, it will be an empty line between other actions in the list.
    /// </summary>
    public static KeyboardAction LineSeparator => new("", "");
}