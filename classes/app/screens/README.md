# Screens #
App screens are used to display a [CLI screen](../../CLI/screens), handle the user input, call the appropriate logic methods and refresh. These screens should not be confused with [CLI screens](../../CLI/screens), which only display a screen based on the parameters and don't handle any inputs. They only render the screen components

Typically, the screens have a *running* boolean variable which determines, if the screen should be refreshed after handling the input, or closed (for example when the user presses the Esc key).

A lot more about handling can be found [here](../handling/README.md). Essentially most of the handling methods return true or false (true if the screen should be refreshed, false - aborted). Consider the simple example from [Deck screen](Deck.cs), which shows info about the deck (from there you can start studying, manage the deck and edit that deck's cards)

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
In case of the [HandleDeck](../handling/Deck.cs) method, the *false* value is returned for example upon deleting the deck (first checking if the user actually accepted the delete dialog by the if statement), or upon pressing Esc key. So when a false value is returned, the loop ends, the screen is closed and goes back to menu
```cs
switch (consoleKey)
        {
            // skipped code
            case ConsoleKey.Delete:
                if (RemoveDeck(database, deck)) return false;
                break;
            // skipped code
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return false;
        }
```

# Front-end handling #
On the example above, the [HandleDeck](../handling/Deck.cs) method simply returns true/false to signify if the screen should be refreshed. However sometimes it is needed to change a local variable based on the user interaction, for example reveal a card on the study screen upon pressing space.

That's why a handling function sometimes return custom status types ([read more here](../handling/README.md)). In such case, the handle function does all the back-end job, but the front-end job, such as revealing the card is done in the screen method directly. Consider the [StudySession](StudySession.cs) screen:
```cs
public static void StudySession(FlashcardsDatabase database, List<Card> cards) {
    // skipped code

    bool running = true;
    bool isCardRevealed = false;

    while (running) {
        Screens.StudySession(
            // skipped screen params
        );
        // Back-end job (if any) is done in HandleStudySession()
        var handleResult = Logic.HandleStudySession(cardChoiceList);

        // Remaining front-end job is done here
        if (handleResult is Logic.HandleStudySessionResult.RevealOrNext) {
            if (isCardRevealed && (cardChoiceList.selectedIndex != cardChoiceList.MaxIndex)) {
                cardChoiceList.MoveForward();
                isCardRevealed = false;
            }
            else isCardRevealed = true;
            
        }
        if (handleResult is Logic.HandleStudySessionResult.MoveBackward) {
            cardChoiceList.MoveBackward();
            isCardRevealed = true;
        }
        if (handleResult is Logic.HandleStudySessionResult.Exit) running = false;
    }

    }
```
In this case, the card revealing is handled inside the StudySession screen. The same goes for moving the card backward (upon the left arrow press). It doesn't change anything on the back-end and needs to change the screen's local variables

# Non-void return types #
In some rare cases the screen may need to return something, so effectively it acts as an upgraded hybrid screen-dialog.
Example of such screen is [CardEditor](CardEditor.cs), which returns a *Card* type of the edited card. By design, the CardEditor screen doesn't change the actual card. It only changes the copy of the card and returns it after modifications. This allows for bigger flexibility.