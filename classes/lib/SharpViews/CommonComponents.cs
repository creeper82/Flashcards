namespace SharpViews;

public static class Components
{
    public static int UiWidth
    {
        get
        {
            try
            {
                return Console.WindowWidth - 2;
            }

            // Default window width if it couldn't be acquired
            catch (Exception)
            {
                return 64;
            }

        }
    }

    public static int UiHeight
    {
        get
        {
            try
            {
                return Console.WindowHeight;
            }

            catch (Exception) { return 32; }
        }
    }

    public static void ClearConsole()
    {
        Console.Clear();
    }

    // Adds margin to before and after string
    public static string Margin(string str, int margin = 1, char marginChar = ' ') => TextPositioning.Margin(str, margin, marginChar);

    private static string Repeat(char ch, int length) => TextPositioning.Repeat(ch, length);

    private static string Spaces(int count) => Repeat(' ', count);

    private static string Truncate(string str, int width) => TextPositioning.Truncate(str, width);

    private static string[] DivideStringIntoArray(this string sourceString, int maxElementLength)
    {
        // check if splitting is needed
        if (sourceString.Length <= maxElementLength) return [sourceString];
        else
        {
            // split the string
            int parts = (int)Math.Ceiling(sourceString.Length / (float)maxElementLength);
            string[] dividedString = new string[parts];

            for (int part = 0; part < parts; part++)
            {
                if (part == parts - 1) dividedString[part] = sourceString[(part * maxElementLength)..];
                else dividedString[part] = sourceString.Substring(part * maxElementLength, maxElementLength);
            }

            return dividedString;
        }

    }

    public static string HorizontalLine(char ch, int? length = null) => CenteredText(Repeat(ch, length ?? UiWidth));

    private static string SingleLineCenteredText(string text, char surroundChar = ' ', bool isFormatted = false) => TextPositioning.CenteredText(text, surroundChar, isFormatted);

    /// <summary>
    /// Aligns <c>text</c> to the screen's right with spaces, or a char provided in <c>surroundChar</c>. A newline is automatically added at the end.
    /// </summary>
    /// <param name="text"Text to align></param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <returns>A string aligned to the right.</returns>
    public static string RightAlignedText(string text, char surroundChar = ' ') => TextPositioning.RightAlignedText(text, surroundChar);

    /// <summary>
    /// Centers each line of the text horizontally. If the text is too long, it is wrapped (moved into new line). A newline is automatically added at the end.
    /// </summary>
    /// <param name="text">Text to center</param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <remarks>If you want to truncate the long text ("..."), instead of wrapping it, use <c>CenteredText</c> instead.</remarks>
    /// <returns>A string with the centered text.</returns>
    public static string CenteredWrappedText(string text, char surroundChar = ' ')
        => CenteredText(string.Join("\n", text.Split("\n").SelectMany(line => line.DivideStringIntoArray(UiWidth))), surroundChar);

    /// <summary>
    /// Centers each line of the text horizontally. <b>If the text is too long, it is truncated ("...").</b> If you use <c>FormattedText</c>,
    /// don't put the colors and tags into <c>text</c>. Instead, add the tags before and after this function.
    /// <para>A newline is automatically added at the end.</para>
    /// </summary>
    /// <param name="text">Text to center</param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <param name="isFormatted">Whether the text is bracket-formatted (see <c>FormattedText</c>). Needed for accurate length calculation.
    /// <b>This is very unstable</b>. Please try to add the formatting tags before and after this function, not into the <c>text</c> argument.</param>
    /// <remarks>If you want to wrap the long text ito new lines instead of truncating it, use <c>CenteredWrappedText</c>. </remarks>
    /// <returns>String with the centered text.</returns>
    public static string CenteredText(string text, char surroundChar = ' ', bool isFormatted = false)
    {
        var lines = text.Split("\n");

        // Center each line separately, and then join the lines with newline (\n)
        return string.Join("\n",
            lines.Select(line => SingleLineCenteredText(line, surroundChar, isFormatted))
        ) + "\n";
    }

    /// <summary>
    /// Creates a visual list out of given strings. Works best when used in conjuction with <c>ChoiceList</c>. The list width will be equal to
    /// the longest element's width. Shorter elements will get additional padding. A newline is automatically added after the list.
    /// </summary>
    /// <param name="sourceStrings">Strings to make the list of.</param>
    /// <param name="selectedIndex">Element index, to which add the selected mark (a dot). By default, in offset to the top element.
    /// Set <c>startIndex</c> to change that offset, e.g. if the list is scrolled. You may use <c>ChoiceList.SelectedIndex</c>.</param>
    /// <param name="startIndex">The index of the top-most element. Only affects the <c>selectedIndex</c> calculation. You may use
    /// <c>ChoiceList.PaginationStartIndex</c>.</param>
    /// <returns></returns>
    public static string List(IEnumerable<string> sourceStrings, int? selectedIndex = null, int startIndex = 0)
    {
        const string NONSELECTED_STRING = "[ ]";
        const string SELECTED_STRING = "[•]";

        // Map the list to add unicode prefixes listed in constants above, depending on the selected element
        List<string> strings = sourceStrings.Select(
            (elem, index) => (index + startIndex == selectedIndex ? SELECTED_STRING : NONSELECTED_STRING) + " " + elem
        ).ToList();

        // Determine the element with largest width
        int listWidth = strings.Max(s => s.Length);

        if (listWidth == 0) return "";

        // Top border
        string list = "\n" + Repeat('-', listWidth);

        // List elements
        foreach (string listElement in strings)
        {
            list += "\n" + listElement;
            // add remaining spaces if necessary
            if (listElement.Length < listWidth) list += Spaces(listWidth - listElement.Length);
        }

        // Bottom border
        list += "\n" + Repeat('-', listWidth);

        return CenteredText(list);
    }

    /// <summary>
    /// Returns a frame with a title, content, and optionally scroll indicators.
    /// </summary>
    /// <param name="inner">The content inside the frame.</param>
    /// <param name="title">The title that will be centered. Leave empty for none.</param>
    /// <param name="horizontalScroll">Whether to display horizontal scroll arrows.</param>
    /// <param name="verticalScroll">Whether to display vertical scroll arrows.</param>
    /// <returns>The frame as string.</returns>
    public static string UiFrame(
        string inner,
        string title = "",
        bool horizontalScroll = false,
        bool verticalScroll = false
    )
    {
        return (
            CenteredText(
                // If title is not empty, then add a margin to it
                title != "" ? Margin(title) : "",
                '-'
            ) + "\n" +
            inner + "\n" +
            (verticalScroll ? RightAlignedText("↑") + RightAlignedText("↓") : "") +
            (horizontalScroll ? RightAlignedText("< >") : "") +
            HorizontalLine('-')
        );

    }

    /// <summary>
    /// A list of keyboard actions. Best displayed under the <c>UiFrame</c> component.
    /// Every action will be shown in a new line. You can use <c>KeyboardAction.LineSeparator</c> to separate actions with an empty line.
    /// </summary>
    /// <param name="options">A list of the keyboard actions to display</param>
    /// <returns>A string with the keyboard actions.</returns>
    public static string KeyboardActionList(List<KeyboardAction> options)
    {
        return string.Join(null, options.Select(option => option + "\n"));
    }
}