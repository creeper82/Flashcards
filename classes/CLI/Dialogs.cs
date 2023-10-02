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
                KeyboardActionList(
                    new() {
                        new KeyboardAction("y", okButton),
                        new KeyboardAction("n", cancelButton)
                    }
                )
            );
        }

        public static void InputScreen(
            string title,
            string message = "",
            string bottomNote = ""
        ) {
            ClearConsole();

            Console.WriteLine(
                UiFrame(
                    inner: MultilineCenteredText(message),
                    title: title
                )
            );
            if (bottomNote != "") Console.WriteLine(bottomNote);
            Console.WriteLine("Leave empty to cancel");
            Console.Write(">>> ");
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

    public static string Input(
        string title,
        string message = "",
        string bottomNote = ""
    )
    {
        DialogScreens.InputScreen(title: title, message: message, bottomNote: bottomNote);
        string userInput = Console.ReadLine() ?? "";

        return userInput;
    }
}