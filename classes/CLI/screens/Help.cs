namespace CLI;

using static Components;

public partial class Screens
{
    internal static void Help(string dbPath = "Unknown path")
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

                    CenteredText("STUDYING") + "\n" +
                    "When in a deck, press Enter to enter a study session. " +
                    "In this mode, the cards aren't revealed until you reveal them.\n\n" +
                    "By default, the cards are tagged as still learning. " +
                    "Ideally, you should tag all the cards that are difficult for you while studying, " +
                    "to later narrow down the study material only to these cards.\n" +
                    "After tagging these cards as \"still learning\", proceed to untag the cards you feel like you've learned. " +
                    "Otherwise, you'd end up studying the same set over and over and never finishing the session.\n\n" +
                    "The end of a study session is when you manage to learn all the tagged cards and untag them\n\n" +

                    CenteredText("PROBLEMS") + "\n" +
                    "There should be no database problems if the app is used correctly.\n" +
                    "Modifying the database with external programs may force the app to be reloaded, " +
                    "or in the worst case break the database structure.\n" +
                    "Most common errors are described and have interactive menus.\n\n" +
                    "However, when you encounter an unknown error, " +
                    "or the app works in some unintended way (crashes when modifying decks, cards), you may need to:\n" +
                    "- fix the database manually, if you're experienced with SQL\n" +
                    "- remove the corrupt database file and let it be recreated\n\n" +
                    "You can find the database file here: " + dbPath + "\n" +
                    "WARNING: Removing the file should be the last resort. It will result in a COMPLETE DATA LOSS\n\n" +

                    CenteredText("AUTHOR") + "\n" +
                    "The whole project is made by me (creeper82), you can see my other projects on GitHub ^^\n" +
                    "Have fun using the app! Report issues on the GitHub page, if any.\n\n" +
                    "Press any key to go back..."
                ),
                title: "Flashcards"
            )
        );
    }
}