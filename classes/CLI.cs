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

    private static string Repeat(char Char, int Len)
    {
        return new String(Char, Len);
    }

    private static string HorizontalLine(char Char)
    {
        return Repeat(Char, UiWidth);
    }

    private static string CenteredText(string text, char SurroundChar = ' ')
    {
        int SurroundLength = (UiWidth - text.Length) / 2;

        return (
            Repeat(SurroundChar, SurroundLength) +
            text +
            Repeat(SurroundChar, SurroundLength)
        );
    }

    private static string MultilineCenteredText(string Text)
    {
        var Lines = Text.Split("\n");
        string LastLine = Lines.Last();
        string Result = "";

        foreach (string Line in Lines)
        {
            Result += CenteredText(Line);
            if (Line != LastLine) Result += "\n";
        }

        return Result;


    }

    private static string UiFrame(string Inner, string Title = "")
    {
        return (
            CenteredText(
                // If title is not empty, then add a margin to it
                Title != "" ? Title.Margin() : "",
                '-'
            ) + "\n\n" +
            Inner + "\n\n" +
            HorizontalLine('-')
        );

    }

    public static void Menu()
    {
        Console.Clear();
        Console.WriteLine(
            UiFrame(
                MultilineCenteredText("Hello. Welcome to Flashcards\nHave a good day"),
                "Flashcards")
        );
    }
}