using Flashcards;
namespace CLI;

using static Components;

public partial class Screens
{
    internal static void Help()
    {
        ClearConsole();

        Console.WriteLine(
            UiFrame(
                inner: (
                    CenteredText("GENERAL") + "\n" +
                    "Welcome to the Flashcards app. This app can serve as your study helper.\n" +
                    "To get started, create a deck, go to the card editor and add some cards. " +
                    "The app controls will always be on the bottom of the screen\n\n" +

                    CenteredText("CONTROLS") + "\n" +
                    "The app is controlled using your keyboard. " +
                    "Most of the keys are repetitive and keep their functions across different screens\n" +
                    "The < > and ↑↓ arrows signify, that either horizontal or vertical scrolling with arrow keys is possible, " +
                    "for example to move your selection in a list\n\n" +
                    "Here are some commonly used controls and their alternatives:\n" +
                    "Enter or SPACE  -  select item / open screen\n" +
                    "R or F2  -  rename currently selected item\n" +
                    "N  -  add a new item\n" +
                    "Esc  -  go back\n\n" +

                    CenteredText("AUTHOR") + "\n" +
                    "The whole project is made by me (creeper82), you can see my other projects there ^^\n" +
                    "Have fun using the app! Report issues on the GitHub page, if any.\n\n" +
                    "Press any key to go back..."
                ),
                title: "Flashcards"
            )
        );
    }
}