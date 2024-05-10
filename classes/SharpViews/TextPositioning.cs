namespace SharpViews;

/// <summary>
/// A set of methods to position the text, especially center it.
/// </summary>
public static class TextPositioning
{
    internal static int UiWidth => Components.UiWidth;
    internal static int UiHeight => Components.UiHeight;

    /// <summary>
    /// Returns a string (doesn't display it to console), where <c>text</c> is surrounded by spaces or other character to fill out the
    /// entire screen width. Use <c>isFormatted = true</c>, if you deal with bracket-formatted text related to the <c>FormattedText</c> class.
    /// <b>Text will be truncated</b> ("...") if it exceeds the console width.
    /// </summary>
    /// <param name="text">The text to center.</param>
    /// <param name="SurroundChar">The character used to surround the text (default: spaces)</param>
    /// <param name="isFormatted">Whether the text is bracket-formatted (see <c>FormattedText</c>). Needed for accurate length calculation.</param>
    /// <remarks>
    /// Use <c>Console.WriteLine()</c> or remember to add a newline when displaying the text somewhere. Filling the whole console line isn't
    /// stable, and sometimes leaves a bit of free space at the line end.
    /// </remarks>
    public static string CenteredText(string text, char SurroundChar = ' ', bool isFormatted = false)
    {
        int textLength = isFormatted ? new FormattedText(text).PureText.Length : text.Length;

        if (text == "") return Repeat(SurroundChar, UiWidth);

        if (textLength > UiWidth) text = Truncate(text, UiWidth, explicitFormattedLength: textLength);

        float surroundLength = (UiWidth - textLength) / 2f;
        if (surroundLength < 0) surroundLength = 0;

        if (isFormatted) return (
            "<instant>" + Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) + "<normal>" +
            text +
            "<instant>" + Repeat(SurroundChar, (int)Math.Floor(surroundLength)) + "\n"
        );

        return (
            Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) +
            text +
            Repeat(SurroundChar, (int)Math.Floor(surroundLength))
        );
    }

    /// <summary>
    /// Repeat a provided character for a provided number of times.
    /// </summary>
    /// <param name="ch">Character to repeat.</param>
    /// <param name="length">Desired repeat count.</param>
    /// <returns></returns>
    internal static string Repeat(char ch, int length) => new(ch, length);

    /// <summary>
    /// Align the provided <c>text</c> to the right side of the screen, and surround it with <c>surroundChar</c> at the left.
    /// </summary>
    /// <param name="text">Text to align.</param>
    /// <param name="surroundChar">Character to surround the text with, from the left. Default: spaces.</param>
    /// <remarks>You <b>must</b> add a new line after the text! Writing to full console width is buggy. </remarks>
    /// <returns>Aligned string.</returns>
    internal static string RightAlignedText(string text, char surroundChar = ' ')
    {
        // if text is empty, simply fill the whole width with surroundChars
        if (text == "") return Repeat(surroundChar, UiWidth);

        int surroundLength = UiWidth - text.Length;

        if (surroundLength < 0) return text;

        return Repeat(surroundChar, surroundLength) + text;
    }

    /// <summary>
    /// Add given <c>marginChar</c>, <c>margin</c> times to the beginning and after the <c>str</c> string.
    /// </summary>
    /// <param name="str">String to apply margin to.</param>
    /// <param name="margin">Length of the margin (both on left and right). Default: 1.</param>
    /// <param name="marginChar">Character to make the margin of. Default: spaces.</param>
    /// <returns></returns>
    internal static string Margin(string str, int margin = 1, char marginChar = ' ')
    {
        return (
            Repeat(marginChar, margin) +
            str +
            Repeat(marginChar, margin)
        );
    }

    /// <summary>
    /// If given string is longer than <c>width</c>, then trim it by adding "..." at the end, and return it.
    /// </summary>
    /// <param name="explicitFormattedLength">If the text is bracket-formatted (<c>FormattedText</c>), then this length will be used for calculations.
    /// Used mainly from within <c>CenteredText</c>. Basically, this length doesn't include bracket tags.</param>
    internal static string Truncate(string str, int width, int? explicitFormattedLength = null)
    {
        if ((explicitFormattedLength ?? str.Length) > width) return str[..(width - 3)] + "...";
        return str;
    }
}