## Controllers (handle methods) #
This folder contains methods of the **Logic** class. Each app screen has a method that handles the user input for that screen and does some logic based on the input. For example - create a new card, or change the sorting method.

It may either only affect the backend, or also modify the interface directly (for example reveal the current card). For this reason, some methods return the **bool** type and some return a custom **status** type to communicate back with the app screen method.

Note that controllers aren't 100% backend. They are a slim line between frontend and backend. There are a lot of operations that can be done both in the screen itself, and in the controller (e.g. moving the `ChoiceList`). The most important thing is to make it readable, consistent, and clean.

However, operations that just scream "backend", like modifying an external database, absolutely should be called from the handle method (controller), not screen!

_Files in the examples are not always updated. They show a general idea of the CLI system, but by the time of reading this, the files have very likely changed._

## Return types ##
The controller (`HandleSomething` method) is supposed to change the backend values, do necessary operations, and return the status back into the frontend (Screen). To change frontend, a handle method may either accept frontend object (such as `ChoiceList`) as a parameter and modify it, or simply return a status that indicates to scroll that list, which will be read by the app screen, and updated accordingly. This duality of options might be somewhat confusing. I generally stick to the approach of returning statuses and setting everything frontend related in the screen itself, but rarely - some dialogs are displayed from within the controller.

### Bool return type ###
Less complicated logic handlers don't affect the screen state variables. They only do certain backendish logic and then either **exit** the screen (in case of Escape key press) **or stay** at the screen to let it refresh (continue the screen loop). A bool is perfect to represent one of two states.

So the returned value is used to determine whether the screen should still be shown, or not. Thus, the handler function value is assigned to the `running` variable which decides if the screen will be refreshed again. See this example from the [Deck screen](../../screens/Deck.cs) (This is the **old version** - before updates)
```cs
public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            CLI.Screens.Deck(deck);
            running = Logic.HandleDeck(database, deck);
        }
    }
```
We want to exit the deck screen when the currently viewed deck is removed, or when the user presses the Escape key. This can be seen in [HandleDeck](Deck.cs) method fragment:
```cs
switch (consoleKey)
        {
            // skipped code
            case ConsoleKey.Delete:
                RemoveDeck(database, deck);
                return false;
            // skipped code
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                return false;
        }
```
The false value is returned, the running variable is set to `false` and the loop ends

### Custom status return type ###
When the true/false (ContinueLoop or Exit) is not enough, aside from changing whether the screen continues running, we also want to modify some local screen variables (so essentially front-end) and not only the back-end logic.
In that case, the Handle method returns a custom status type, which is later handled not in the back-end `Logic` class, but in the screen directly. Consider the following example:

In the [StudySession screen](../../screens/StudySession.cs) the spacebar key is used to reveal the current card's back side. So the frontend screen's local `isRevealed` boolean variable must be modified upon handling the spacebar key. A simple true/false is not enough, because there are more events that can happen
than just "continue" and "exit"

For that reason the [HandleStudySession](StudySession.cs) method returs a custom `HandleStudySessionResult` enum:
```cs
public enum HandleStudySessionResult
{
    ContinueLoop,
    RevealOrNext,
    MoveBackward,
    RestartSession,
    ContinueOnlyTagged,
    Exit
}
```


As said earlier, revealing the onscreen card must be done from the screen scope, so it is handled in [StudySession screen](../screens/StudySession.cs) the following way:
```cs
var handleResult = Logic.HandleStudySession(cardChoiceList);

if (handleResult == Logic.HandleStudySessionResult.RevealOrNext) {
    if (isCardRevealed && (cardChoiceList.SelectedIndex != cardChoiceList.MaxIndex)) {
        cardChoiceList.MoveForward();
        isCardRevealed = false;
    }
    else isCardRevealed = true;
}
// the rest of status checking
```

And here's how that `RevealOrNext` status is returned in the [HandleStudySession](StudySession.cs) method:
```cs
switch (consoleKey)
    {
        // skipped code
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
            return HandleStudySessionResult.RevealOrNext;
        // skipped code
    }
```

### Handle in frontend or backend - controversy
In some handling functions, the `ChoiceList` is not controlled by using a custom status, but is passed as an argument to the Handle function and for example moved forward/backward inside the handle function, and not in the frontend screen like on the example with `RevealOrNext` above. This is not super clean, but strongly reduces the amount of enum classes where they are unnecessary. However, the ideal way would be to return a custom handle enum value such as `MoveForward` and handle it in screen, where front-end should be handled.

But simple front-end variables like boolean (`isRevealed`), int, string etc should 100% be controlled on the front-end. The `ChoiceList` is one of these things which technically can be controlled in either the screen, or the controller.

### Bool or a custom return type? ###
Bool and custom return types are almost the same thing, but written differently. Basically, a bool return type is equivalent to making a custom enum with return values like:

```
ContinueLoop, Exit
```

And in more advanced apps, just two statuses aren't enough. You would end up with custom enum return types anyway. Bool is for a really simple app.

## Actions ##
When an user interactions requires more than just one or two code lines (for example when a dialog needs to be shown and some conditions must be checked), the code should be wrapped into a method of the `Logic` class in the [Actions](../actions/) folder.

Consider the action of renaming a deck. Here's the current code for it, located in [DeckActions](../actions/DeckActions.cs) under the `RenameDeck` method. That'd make the `switch(consoleKey) { ... }` statement unnecessarily long, so it was separated into a method
```cs
private static Deck RenameDeck(FlashcardsDatabase database, Deck deck)
    {
        string newName = CLI.Dialogs.Input(
            title: "Rename deck",
            message: $"Enter a new name for deck: {deck.Name}"
        ).Trim();

        if (newName != "") database.RenameDeck(deck, newName);

        return deck;
    }
```

And then called from the [HandleMenu](Menu.cs) handler, when the user clicks the F2 or R key:
```cs
switch (consoleKey)
    {
        // skipped code
        case ConsoleKey.R:
        case ConsoleKey.F2:
            Deck renamedDeck = RenameDeck(database, deckChoiceList.SelectedChoice);
            deckChoiceList.MoveToChoice(renamedDeck);
            break;
    }
```
A `RenameDeck` call would be enough, but to improve UX the choice list is automatically moved to the newly renamed deck, for example, if due to the sorting it'll change it's position in the deck list.

The deck list utilizes the `ChoiceList` class, which is documented [there](../../../CLI/). In this case the `MoveToChoice` method is used. ChoiceList is generally used for all the horizontal and vertical lists in this app, as a part of my simple `SharpViews` library