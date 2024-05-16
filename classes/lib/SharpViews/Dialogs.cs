namespace SharpViews;

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
                    inner: CenteredText(message) + "\n",
                    title: title
                )
            );
            // Display options
            Console.WriteLine(
                KeyboardActionList(
                    [
                        new KeyboardAction("y", okButton),
                        new KeyboardAction("n", cancelButton)
                    ]
                )
            );
        }

        public static void InputScreen(
            string title,
            string message = "",
            string bottomNote = ""
        )
        {
            ClearConsole();

            Console.WriteLine(
                UiFrame(
                    inner: CenteredText(message),
                    title: title
                )
            );
            if (bottomNote != "") Console.WriteLine(bottomNote);
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

        switch (ConsoleInput.GetConsoleKey())
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
        string bottomNote = "Leave empty to cancel"
    )
    {
        DialogScreens.InputScreen(title: title, message: message, bottomNote: bottomNote);
        string userInput = Console.ReadLine() ?? "";

        return userInput;
    }
    
    /// <summary>
    /// Displays an error screen with options to retry or cancel.
    /// </summary>
    /// <param name="windowTitle">The title at the top of the error screen.</param>
    /// <param name="errorText">Text contents of the error that will be displayed.</param>
    /// <returns>A boolean determining whether the user wants to retry the action that caused an error.</returns>
    public static bool ErrorScreen(string windowTitle = "ERROR", string errorText = "Error was not recognized. Sorry") => ErrorHandling.ErrorScreen(windowTitle, errorText);
}