namespace CLI;

using static Components;

public static class Dialogs
{
    private static class DialogScreens
    {
        public static void ConfirmScreen(
            string title,
            string message = "",
            string okButton = "OK",
            string cancelButton = "Cancel"
        )
        {
            ClearConsole();
            // Display confirmation dialog
            Console.WriteLine(
                UiFrame(
                    inner: MultilineCenteredText(message) + "\n",
                    title: title
                )
            );
            // Display options
            Console.WriteLine(
                OptionList(
                    new() {
                        new Option("y", okButton),
                        new Option("n", cancelButton)
                    }
                )
            );
        }
    }

    public static bool Confirm(
        string title,
        string message = "",
        string okButton = "OK",
        string cancelButton = "Cancel"
    )
    {
        DialogScreens.ConfirmScreen(title, message, okButton, cancelButton);

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Y:
                return true;
            default:
                return false;
        }
    }
}