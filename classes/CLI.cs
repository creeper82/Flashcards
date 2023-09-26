namespace CLI;

public static class CLI {
    public static int UiWidth = 64;

    private static string Repeat(char Char, int Len) {
        return new String(Char, Len);
    }

    private static string CenteredText(string text, char SurroundChar = ' ') {
        int SurroundLength = UiWidth - (text.Length * 2);
        return (
            Repeat(SurroundChar, SurroundLength) +
            text +
            Repeat(SurroundChar, SurroundLength)
            );
    }

    public static void UiFrame(string Inner, string Title = "") {
        

    }

    public static void Menu() {

    }
}