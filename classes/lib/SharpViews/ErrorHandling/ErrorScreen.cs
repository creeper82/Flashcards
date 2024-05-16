namespace SharpViews;

using static SharpViews.Components;

public static partial class ErrorHandling {
    /// <summary>
    /// Displays an error screen with options to retry or cancel.
    /// </summary>
    /// <param name="windowTitle">The title at the top of the error screen.</param>
    /// <param name="errorText">Text contents of the error that will be displayed.</param>
    /// <returns>A boolean determining whether the user wants to retry the action that caused an error.</returns>
    public static bool ErrorScreen(string windowTitle = "ERROR", string errorText = "Error was not recognized. Sorry")
    {
        bool running = true;

        while (running)
        {
            CLIErrorScreen(
                windowTitle: windowTitle,
                errorText: errorText
            );

            var handleResult = HandleErrorScreen();

            if (handleResult == HandleErrorScreenResult.Retry) return true;
            if (handleResult == HandleErrorScreenResult.Cancel) return false;
        }

        return false;
    }

    private static void CLIErrorScreen(string windowTitle = "ERROR", string errorText = "Error was not recognized. Sorry")
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: "An error occured.\n" +
                errorText,

                title: windowTitle,
                verticalScroll: false
            )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(errorScreenKeyboardActions)
        );
    }

    private enum HandleErrorScreenResult {
        ContinueLoop, Retry, Cancel
    }

    private static HandleErrorScreenResult HandleErrorScreen() {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.Spacebar => HandleErrorScreenResult.Retry,
            ConsoleKey.Escape => HandleErrorScreenResult.Cancel,
            _ => HandleErrorScreenResult.ContinueLoop,
        };
    }

    private static readonly List<KeyboardAction> errorScreenKeyboardActions = [new("space", "retry"), new("esc", "cancel")];
}