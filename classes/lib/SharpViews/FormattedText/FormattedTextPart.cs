namespace SharpViews;

/// <summary>
/// A part of <c>FormattedText</c>, with custom parameters like display color, speed. Should not be created manually.
/// To create and display a formatted text, look at <c>FormattedText</c> class.
/// </summary>
/// <param name="text">The text.</param>
/// <param name="textColor">The color that will be displayed in the console. Default is gray.</param>
/// <param name="textSpeed">The speed in which the text will be displayed to console.</param>
public class FormattedTextPart(string text, ConsoleColor textColor = ConsoleColor.Gray, TextSpeed textSpeed = TextSpeed.Normal)
{
    public string Text { get; set; } = text;
    public ConsoleColor TextColor { get; set; } = textColor;
    public TextSpeed Speed { get; set; } = textSpeed;

    /// <summary>
    /// Displays the text part, according to its' color, and display speed (add <c>false</c> parameter to disable).
    /// </summary>
    /// <param name="useSpeed">
    /// Whether to account for text display speed. By default <c>false</c>. Set to <c>true</c> to render text char by char according to set speed.
    /// </param>
    /// <param name="speedMultiply">
    /// Additionally make the text drawing x times faster.
    /// </param>
    public void WriteToConsole(bool useSpeed = false, float speedMultiply = 1)
    {
        var prevColor = Console.ForegroundColor;

        Console.ForegroundColor = TextColor;

        if (useSpeed)
        {
            foreach (char ch in Text)
            {
                Console.Write(ch);
                Thread.Sleep((int)((int)Speed / speedMultiply));
            }
        }
        else
        {
            Console.Write(Text);
        }

        Console.ForegroundColor = prevColor;
    }

    public FormattedTextPart Copy() => new(Text, TextColor, Speed);

}