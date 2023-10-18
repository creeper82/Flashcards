# Screens #
App screens are used to display a CLI screen, handle the user input, call the appropriate logic methods and refresh. These screens should not be confused with [CLI screens](../../CLI/Screens.cs), which only display a screen based on the parameters and don't handle any inputs. They only render the screen components

Typically, the screens have a *running* boolean variable which determines if the screen should be refreshed after handling the input, or closed (for example when the user presses the Esc key).

A lot more about handling can be found [here](../handling/README.md), essentially most of the handling methods return true or false (true if the screen should be refreshed, false - aborted). Consider the simple example from [Deck screen](Deck.cs), which shows info about the deck (from there you can start studying, manage the deck and edit that deck's cards)

```cs
public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            //this renders the deck screen
            Screens.Deck(deck);
            // this handles the user input and does appropriate logic
            running = Logic.HandleDeck(database, deck);
        }

    }
```
In case of the *HandleDeck* method, the *false* value is returned for example upon deleting the deck (first checking if the user actually accepted the delete dialog by the if statement) or upon pressing Esc key:
```cs
switch (consoleKey)
        {
            // skipped code
            case ConsoleKey.Delete:
                if(RemoveDeck(database, deck)) return false;
                break;
            // skipped code
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return false;
        }
```