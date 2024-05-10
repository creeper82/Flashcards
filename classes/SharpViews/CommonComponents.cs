namespace SharpViews;

public static class ConsoleInput
{
    public static ConsoleKey GetConsoleKey()
    {
        return Console.ReadKey(true).Key;
    }

    public static void WaitForAnyKey()
    {
        Console.ReadKey(true);
    }
}

public static class Components
{
    internal static int UiWidth
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

    internal static int UiHeight
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

    internal static void ClearConsole()
    {
        Console.Clear();
    }

    // Adds margin to before and after string
    internal static string Margin(string str, int margin = 1, char marginChar = ' ') => TextPositioning.Margin(str, margin, marginChar);

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

    internal static string HorizontalLine(char ch, int? length = null) => CenteredText(Repeat(ch, length ?? UiWidth));

    private static string SingleLineCenteredText(string text, char surroundChar = ' ', bool isFormatted = false) => TextPositioning.CenteredText(text, surroundChar, isFormatted);

    internal static string RightAlignedText(string text, char surroundChar = ' ') => TextPositioning.RightAlignedText(text, surroundChar);

    /// <summary>
    /// Centers each line of the text horizontally. If the text is too long, it is wrapped (moved into new line).
    /// </summary>
    /// <param name="text">Text to center</param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <remarks>If you want to truncate the long text ("..."), instead of wrapping it, use <c>CenteredText</c> instead.</remarks>
    /// <returns>String with the centered text.</returns>
    internal static string CenteredWrappedText(string text, char surroundChar = ' ')
        => CenteredText(string.Join("\n", text.Split("\n").SelectMany(line => line.DivideStringIntoArray(UiWidth))), surroundChar);

    /// <summary>
    /// Centers each line of the text horizontally. <b>If the text is too long, it is truncated ("...")</b>
    /// </summary>
    /// <param name="text">Text to center</param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <remarks>If you want to wrap the long text ito new lines instead of truncating it, use <c>CenteredWrappedText</c>. </remarks>
    /// <returns>String with the centered text.</returns>
    internal static string CenteredText(string text, char surroundChar = ' ', bool isFormatted = false)
    {
        var lines = text.Split("\n");

        // Center each line separately, and then join the lines with newline (\n)
        return string.Join("\n", 
            lines.Select(line => SingleLineCenteredText(line, surroundChar, isFormatted))
        );
    }

    /// <summary>
    /// Creates a visual list out of given strings. Works best when used in conjuction with <c>ChoiceList</c>. The list width will be equal to
    /// the longest element's width. Shorter elements will get additional padding.
    /// </summary>
    /// <param name="sourceStrings">Strings to make the list of.</param>
    /// <param name="selectedIndex">Element index, to which add the selected mark (a dot). By default, in offset to the top element.
    /// Set <c>startIndex</c> to change that offset, e.g. if the list is scrolled.</param>
    /// <param name="startIndex">The index of the top-most element. Only affects the <c>selectedIndex</c> calculation.</param>
    /// <returns></returns>
    internal static string List(IEnumerable<string> sourceStrings, int? selectedIndex = null, int startIndex = 0)
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

        // Elements
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

    internal static string UiFrame(
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
            ) + "\n\n" +
            inner + "\n" +
            (verticalScroll ? RightAlignedText("↑") + "\n" + RightAlignedText("↓") : "") +
            (horizontalScroll ? RightAlignedText("< >") : "") + "\n" +
            HorizontalLine('-')
        );

    }

    internal static string KeyboardActionList(List<KeyboardAction> options)
    {
        return string.Join(null, options.Select(option => option + "\n"));
    }
}