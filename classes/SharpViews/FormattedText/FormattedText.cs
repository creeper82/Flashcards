namespace SharpViews;

using static TextPositioning;

/// <summary>
/// <para>Represents a list of formatted text parts, which together create one text with various formattings, like colors.
/// Can be created from a list of these text parts, or decoded from a human-readable string. </para>
/// <para>Quick example: <c>new FormattedText("{green}Hello{/}, {yellow}World{/}").DisplayFormatted()</c>.
/// Warning: <b>use angled (triangle) brackets, like in HTML!</b> </para>
/// <para>Alternative syntax: <c>FormattedText.DisplayFormatted("your text")</c></para>
/// </summary>
/// <param name="textParts">The list of <c>FormattedTextPart</c> objects that together create a whole text.</param>

public partial class FormattedText(List<FormattedTextPart> textParts)
{
    List<FormattedTextPart> TextParts { get; set; } = textParts;

    // Secondary constructor - you can create a formatted text from a string this:
    // "Hello world. My name is <green>John</>. Here are some colors: <yellow>yellow<blue>blue"

    /// <summary>
    /// Creates a formatted text instance based on the provided human-readable text.
    /// </summary>
    /// <param name="bracketFormattedText">
    /// The text to be formatted, with tags in brackets. Example: "{green}Hello{/} {yellow}world{/}"  (use angle brackets, like
    /// in HTML and XML!).
    /// </param>

    public FormattedText(string bracketFormattedText, bool centered = false) : this(FormattedTextDecoder.From(
        centered ? CenteredText(bracketFormattedText, isFormatted: true) : bracketFormattedText
    )) { }

    public string PureText => string.Concat(TextParts.Select(part => part.Text));

    /// <summary>
    /// Displays the formatted text to console. Alternative syntax: <c>FormattedText.DisplayFormatted("...");</c>
    /// </summary>
    /// <param name="newLine">Whether to place a new line after all the text. By default <c>false</c>.</param>
    /// <param name="useSpeed">Whether to account for speed while displaying text. By default <c>true</c></param>
    /// <param name="QuickDraw">Use to additionally speed up the drawing speed, for example for descriptions already seen before</param>
    public void DisplayFormatted(bool newLine = false, bool useSpeed = true, bool quickDraw = false)
    {
        foreach (var textPart in TextParts) textPart.WriteToConsole(useSpeed, speedMultiply: quickDraw ? 4f : 1);
        if (newLine) Console.WriteLine();
    }

    /// <summary>
    /// Displays the formatted text to console.
    /// Alternative syntax: <c>new FormattedText("...").DisplayFormatted();</c>
    /// </summary>
    /// <param name="newLine">Whether to place a new line after all the text. By default <c>false</c>.</param>
    /// <param name="useSpeed">Whether to account for speed while displaying text. By default <c>true</c></param>
    /// <param name="QuickDraw">Use to additionally speed up the drawing speed, for example for descriptions already seen before</param>
    public static void DisplayFormatted(string text, bool newLine = false, bool useSpeed = true, bool quickDraw = false) {
        new FormattedText(text).DisplayFormatted(newLine, useSpeed, quickDraw);
    }
}