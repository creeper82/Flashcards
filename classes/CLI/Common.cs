using Flashcards;

namespace CLI;

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

    internal static void ClearConsole()
    {
        try
        {
            Console.Clear();
        }
        // Other method to clear console
        catch (Exception)
        {
            Console.Write("\x1B[2J\x1B[H");
        }
    }

    // Adds margin to before and after string
    internal static string Margin(this string Str, int Margin = 1, char MarginChar = ' ')
    {
        return (
            Repeat(MarginChar, Margin) +
            Str +
            Repeat(MarginChar, Margin)
        );
    }

    private static string Repeat(char Char, int length)
    {
        return new string(Char, length);
    }

    private static string Spaces(int count)
    {
        return Repeat(' ', count);
    }

    internal static string HorizontalLine(char Char, int? length = null)
    {
        return CenteredText(Repeat(Char, length ?? UiWidth));
    }

    internal static string CenteredText(string text, char SurroundChar = ' ')
    {

        if (text == "") return Repeat(SurroundChar, UiWidth);

        float surroundLength = (UiWidth - text.Length) / 2f;

        return (
            Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) +
            text +
            Repeat(SurroundChar, (int)Math.Floor(surroundLength))
        );
    }

    internal static string MultilineCenteredText(string Text)
    {
        var lines = Text.Split("\n");
        string result = "";

        for (int i = 0; i < lines.Length; i++)
        {
            result += CenteredText(lines[i]);
            if (i < lines.Length - 1) result += "\n";
        }

        return result;


    }

    internal static string DeckCard(Card card, bool revealed = false)
    {
        int maxWidth = Math.Max(card.Front.Length, card.Back.Length);

        if (!revealed)
        {
            return MultilineCenteredText(card.Front);
        }
        else {
            return (
                MultilineCenteredText(card.Front) + "\n" +
                HorizontalLine('-', Math.Min(maxWidth + 4, UiWidth)) + "\n" +
                MultilineCenteredText(card.Back) + "\n"
            );
        }
    }

    internal static string DeckList(IEnumerable<Deck> decks, int? selectedDeckIndex = null, int startIndex = 0)
    {
        var list = decks.Select(deck => deck.Name);
        return List(list, selectedDeckIndex, startIndex);
    }

    // Create a list with top+bottom dashed border and elements inside
    internal static string List(IEnumerable<string> sourceStrings, int? selectedIndex = null, int startIndex = 0)
    {
        int listWidth = 0;

        const string NONSELECTED_STRING = "[ ]";
        const string SELECTED_STRING = "[•]";

        // Map the list to add unicode prefixes listed in constants above, depending on the selected element
        List<string> strings = sourceStrings.Select((elem, index) => (index + startIndex == selectedIndex ? SELECTED_STRING : NONSELECTED_STRING) + " " + elem).ToList();


        // Determine the element with largest width
        foreach (string listElement in strings)
        {
            if (listElement.Length > listWidth) listWidth = listElement.Length;
        }

        if (listWidth == 0) return "";
        else
        {
            // Top border
            string list = "\n" + Repeat('-', listWidth);

            // Elements
            foreach (string listElement in strings)
            {
                list += "\n" + listElement;
                if (listElement.Length < listWidth) list += Spaces(listWidth - listElement.Length);
            }

            // Bottom border
            list += "\n" + Repeat('-', listWidth);

            return MultilineCenteredText(list);
        }
    }

    internal static string UiFrame(string inner, string title = "")
    {
        return (
            CenteredText(
                // If title is not empty, then add a margin to it
                title != "" ? title.Margin() : "",
                '-'
            ) + "\n\n" +
            inner + "\n\n" +
            HorizontalLine('-')
        );

    }

    internal static string KeyboardActionList(List<KeyboardAction> options)
    {
        string result = "";

        foreach (KeyboardAction option in options)
        {
            result += option.ToString() + "\n";
        }

        return result;
    }
}