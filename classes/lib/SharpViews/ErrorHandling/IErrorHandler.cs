using System.Text.Json;

namespace SharpViews;

public static partial class ErrorHandling
{
    /// <summary>
    /// Error handler is used to convert an exception into a friendly error text. You must implement one yourself.
    /// </summary>
    public interface IErrorHandler
    {
        static abstract string GetErrorText(Exception e);
    }

    /// <summary>
    /// The most basic error handler, which simply returns the content of the excpetion message, no matter what kind of exception happens.
    /// </summary>
    public class PlainErrorHandler : IErrorHandler {
        public static string GetErrorText(Exception e) => "An error occured. Message:\n" + e.Message;
    }

    /// <summary>
    /// This general-use error handler covers common HttpClient exceptions, such as timeout, and no internet connection.
    /// </summary>
    public class HttpErrorHandler : IErrorHandler
    {
        public static string GetErrorText(Exception e)
        {
            if (e.InnerException is TimeoutException) return "The request has timed out. Check internet connection, or try again.";
            if (e is HttpRequestException e1) return "Error communicating with the server. Check internet connection.\nMessage: " + e1.Message;
            if (e is JsonException) return "Error while reading the data. Could not parse JSON";
            return "Unrecognized error occured. Sorry";
        }
    }
}