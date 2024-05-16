namespace SharpViews;

public static partial class ErrorHandling
{
    /// <summary>
    /// Tries to execute the given <c>action</c> (i.e. a function that returns nothing). If it fails (exception occurs),
    /// an error message is displayed, with a possibility to retry the action, or quit. The action can be retried as many times as the user wants to.
    /// Error handling is done based on the given <c>ErrorHandler</c> in generic type param.
    /// </summary>
    /// <typeparam name="ErrorHandler">An error handler, which implements <c>IErrorHandler</c>. It is used to generate a friendly
    /// message based on the occured exception. For a most basic one, use <c>PlainErrorHandler</c></typeparam>
    /// <param name="action">Action that will be executed, while monitoring for exceptions.</param>
    /// <returns>
    /// <c>true</c>, if executed successfully.
    /// <c>false</c> if it failed, and the user <b>choosed not to retry it anymore.</b>
    /// </returns>
    public static bool TryOrHandleError<ErrorHandler>(Action action) where ErrorHandler : IErrorHandler
    {
        while (true)
        {
            try { action.Invoke(); return true; }

            catch (Exception e)
            {
                if (!Dialogs.ErrorScreen(errorText: ErrorHandler.GetErrorText(e))) return false;
            }
        }
    }

    /// <summary>
    /// Tries to execute a given <c>function</c> and retrieve the return value of <c>ReturnType</c> (second generic type param) from it.
    /// If it fails (exception occurs),
    /// an error message is displayed, with a possibility to retry the function, or quit. The function can be retried as many times
    /// as the user wants to. Error handling is done based on the given <c>ErrorHandler</c> in first generic type param.
    /// </summary>
    /// <typeparam name="ErrorHandler">An error handler, which implements <c>IErrorHandler</c>. It is used to generate a friendly
    /// message based on the occured exception. For a most basic one, use <c>PlainErrorHandler</c></typeparam>
    /// <typeparam name="ReturnType">The type that is returned by the given function. It must be a class type, because it must support
    /// null return type. Return values like ints are not supported at the moment</typeparam>
    /// <param name="function">Function that will be executed, while monitoring for exceptions.</param>
    /// <returns>
    /// Actual value returned by the <c>function</c>, if executed successfully.
    /// <c>null</c>, if it fails.
    /// <para>The return type is nullable! So if your function returns <c>string</c>, then <c>TryGetOrHandleError</c> will return <c>string?</c></para>
    /// </returns>
    /// <remarks>
    /// See <c>TryOrHandleError</c> for actions that don't return values. See <c>TryGetOrHandleErrorAsync</c> for Tasks (e.g. API call async)
    /// </remarks>
    public static ReturnType? TryGetOrHandleError<ErrorHandler, ReturnType>(Func<ReturnType> function) where ErrorHandler : IErrorHandler where ReturnType : class
    {
        while (true)
        {
            try { return function.Invoke(); }

            catch (Exception e)
            {
                if (!Dialogs.ErrorScreen(errorText: ErrorHandler.GetErrorText(e))) return null;
            }
        }
    }

    /// <summary>
    /// Tries to execute a given <c>function</c> which is a <c>Task</c> (<b>is async</b>) and retrieve the return value of <c>ReturnType</c> (second generic type param)
    /// from it (awaits for the result). If it fails (exception occurs),
    /// an error message is displayed, with a possibility to retry the function, or quit. The function can be retried as many times
    /// as the user wants to. Error handling is done based on the given <c>ErrorHandler</c> in first generic type param.
    /// </summary>
    /// <typeparam name="ErrorHandler">An error handler, which implements <c>IErrorHandler</c>. It is used to generate a friendly
    /// message based on the occured exception. For a most basic one, use <c>PlainErrorHandler</c></typeparam>
    /// <typeparam name="ReturnType">The type that is returned by the given function. It must be a class type, because it must support
    /// null return type. Return values like ints are not supported at the moment</typeparam>
    /// <param name="function">Async function that will be executed, while monitoring for exceptions.</param>
    /// <param name="loadingText">Whether to write "Loading..." to console, when the function is being executed.</param>
    /// <returns>
    /// Actual value returned by the <c>function</c>, if executed successfully.
    /// <c>null</c>, if it fails.
    /// <para>The return type is nullable! So if your function returns <c>string</c>, then <c>TryGetOrHandleErrorAsync</c> will return <c>string?</c></para>
    /// </returns>
    /// <remarks>
    /// See <c>TryOrHandleError</c> for actions that don't return values. See <c>TryGetOrHandleError</c> for normal, not async functions.
    /// </remarks>
    public static async Task<ReturnType?> TryGetOrHandleErrorAsync<ErrorHandler, ReturnType>(
    Func<Task<ReturnType>> function, bool loadingText = false) where ErrorHandler : IErrorHandler where ReturnType : class
    {
        while (true)
        {
            try
            {
                if (loadingText) Console.Write("\nLoading...");
                return await function.Invoke();
            }
            catch (Exception e)
            {
                if (!Dialogs.ErrorScreen(errorText: ErrorHandler.GetErrorText(e))) return null;
            }
        }
    }
}