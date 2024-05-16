namespace SharpViews;

public static class ConsoleInput
{
    /// <summary>
    /// Retrieves the next pressed console key, while clearing the buffer of any queued ones (see <c>clearBuffer</c> param).
    /// </summary>
    /// <param name="clearBuffer">Whether to clear any keys that are queued, for example pressed when the screen was loading.</param>
    /// <returns>The pressed console key.</returns>
    public static ConsoleKey GetConsoleKey(bool clearBuffer = true)
    {
        if (clearBuffer) ClearBuffer();
        return Console.ReadKey(true).Key;
    }

    /// <summary>
    /// Clears the buffer (queue) of pressed console keys, e.g. during long loading screen, so they won't be immediately picked up on next
    /// console key retrieval.
    /// </summary>
    public static void ClearBuffer() {
        while (Console.KeyAvailable) Console.ReadKey(false);
    }

    /// <summary>
    /// Waits for any key, while also clearing the buffer of any queued ones (see <c>clearBuffer</c> param).
    /// </summary>
    /// <param name="clearBuffer">Whether to clear any keys that are queued, for example pressed when the screen was loading.</param>
    public static void WaitForAnyKey(bool clearBuffer = true)
    {
        if (clearBuffer) ClearBuffer();
        Console.ReadKey(true);
    }
}