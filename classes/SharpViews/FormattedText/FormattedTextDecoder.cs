namespace SharpViews;

public partial class FormattedText
{
    private static class FormattedTextDecoder
    {
        /// <summary>
        /// Create a list of formatted text parts based on a human-readable string input with HTML-like tags.
        /// </summary>
        /// <param name="text">The human-readable text.</param>
        /// <returns></returns>
        public static List<FormattedTextPart> From(string text)
        {
            if (text == "") return [];

            List<FormattedTextPart> allTextParts = [];
            FormattedTextPart currentTextPart = new("");
            bool inBracket = false;
            int currentPos = 0;

            int nextOpenBracket = text.IndexOf('<');
            int nextCloseBracket = text.IndexOf('>');

            while ((nextOpenBracket != -1 || (nextCloseBracket != -1 && inBracket)) && currentPos < text.Length - 1)
            {
                if (inBracket)
                {

                    string bracketText = text[currentPos..nextCloseBracket];
                    UpdateAccordingToTag(currentTextPart, bracketText);
                    currentPos = nextCloseBracket + 1;

                    inBracket = false;

                    nextOpenBracket = text.IndexOf('<', currentPos);
                }
                else
                {
                    string textUntilBracket = text[currentPos..nextOpenBracket];
                    currentTextPart.Text += textUntilBracket;
                    currentPos = nextOpenBracket + 1;

                    inBracket = true;

                    allTextParts.Add(currentTextPart.Copy());

                    nextCloseBracket = text.IndexOf('>', currentPos);
                }
            }

            // add the remaining text after last tag (if any)
            if (currentPos < text.Length)
            {
                currentTextPart.Text = text[currentPos..];
                allTextParts.Add(currentTextPart.Copy());
            }

            return allTextParts;
        }

        // This sets how the <tags> affect FormattedTextPart object
        private static void UpdateAccordingToTag(FormattedTextPart textPart, string tag)
        {
            textPart.Text = "";

            switch (tag.ToLower())
            {
                case "green":
                    textPart.TextColor = ConsoleColor.Green; break;
                case "yellow":
                    textPart.TextColor = ConsoleColor.Yellow; break;
                case "red":
                    textPart.TextColor = ConsoleColor.Red; break;
                case "blue":
                    textPart.TextColor = ConsoleColor.Blue; break;
                case "cyan":
                    textPart.TextColor = ConsoleColor.Cyan; break;
                case "magenta":
                    textPart.TextColor = ConsoleColor.Magenta; break;
                case "/color":
                case "/c":
                    textPart.TextColor = ConsoleColor.Gray; break;
                case "instant":
                    textPart.Speed = TextSpeed.Instant; break;
                case "fast":
                    textPart.Speed = TextSpeed.Fast; break;
                case "normal":
                case "/s":
                    textPart.Speed = TextSpeed.Normal; break;
                case "slow":
                    textPart.Speed = TextSpeed.Slow; break;
                case "veryslow":
                    textPart.Speed = TextSpeed.VerySlow; break;
                case "/":
                    textPart.TextColor = ConsoleColor.Gray;
                    textPart.Speed = TextSpeed.Normal;
                    break;
            }
        }
    }
}