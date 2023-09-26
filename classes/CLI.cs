using Flashcards;

namespace CLI;

public static class CLI
{
    private static int UiWidth
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
        set { }
    }

    // Adds margin to before and after string
    private static string Margin(this string Str, int Margin = 1, char MarginChar = ' ')
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

    private static string HorizontalLine(char Char)
    {
        return Repeat(Char, UiWidth);
    }

    private static string CenteredText(string text, char SurroundChar = ' ')
    {

        if (text == "") return Repeat(SurroundChar, UiWidth);

        float surroundLength = (UiWidth - text.Length) / 2f;

        Console.WriteLine($"\n{text} -- ui width is {UiWidth}, surround length is {surroundLength}, text width is {text.Length}\n");

        return (
            Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) +
            text +
            Repeat(SurroundChar, (int)Math.Floor(surroundLength))
        );
    }

    private static string MultilineCenteredText(string Text)
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

    private static string DeckList(IEnumerable<Deck> decks)
    {
        var list = decks.Select(deck => deck.Name);
        return List(list);
    }

    // Create a list with top+bottom dashed border and elements inside
    private static string List(IEnumerable<String> strings)
    {
        int width = 0;

        // Determine element with largest width
        foreach (string elem in strings)
        {
            if (elem.Length > width) width = elem.Length;
        }

        if (width == 0) return "";
        else
        {
            // Top border
            string list = "\n" + Repeat('-', width);

            // Elements
            foreach (string elem in strings)
            {
                list += "\n" + elem;
                if (elem.Length < width) list += Spaces(width - elem.Length);
            }

            // Bottom border
            list += "\n" + Repeat('-', width);

            return MultilineCenteredText(list);
        }
    }

    private static string UiFrame(string inner, string title = "")
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

    public static void Menu(IEnumerable<Deck> decks)
    {
        Console.Clear();
        Console.WriteLine(
            UiFrame(
                (
                    MultilineCenteredText("Welcome to Flashcards!\nHere are your decks:\n") +
                    DeckList(decks)
                ),
                "Flashcards")
        );
    }
}