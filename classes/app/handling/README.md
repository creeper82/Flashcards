## Handling #
This folder contains methods of the **Logic** class. Each app screen has a method that handles the user input for that screen and does some logic based on the input. For example - create a new card, or change the sorting method.

It may either only affect the backend, or also modify the screen state directly (for example reveal the current card). For this reason, some methods return the **bool** type and some return a custom **status** type.

## Return types ##

### Bool return type ###
Less complicated logic handlers, such as [HandleDeck method](Deck.cs) don't affect the screen state variables. They only do certain backendish logic and then either **exit** the screen (in case of Escape key press) **or stay** at the screen to let it refresh (continue the screen loop).

So the returned value is used to determine whether the screen should still be shown, or not. Thus, the handler function value is assigned to the _running_ variable which decides if the screen will be refreshed again. See this example from the [Deck screen](../screens/Deck.cs):
```cs
public static void Deck(FlashcardsDatabase database, Deck deck)
    {
        bool running = true;

        while (running)
        {
            Screens.Deck(deck);
            running = Logic.HandleDeck(database, deck);
        }

    }
```
This is the [Deck.cs](../screens/Deck.cs) screen. We want to exit the deck screen when the currently viewed deck is removed, or when the user presses the Escape key. This can be seen in [HandleDeck method](Deck.cs) fragment:
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

### Custom status return type ###
When the true/false is not enough, aside from changing whether the screen continues running, we also want to modify some local screen variables (so essentially front-end) and not only the back-end logic.
In that case, the Handle method returns a custom status type, which is later handled not in the *Logic* class, but in the screen directly. Consider the following example:

In the [StudySession screen](../screens/StudySession.cs) the spacebar key is used to reveal the current card's back side. So the screen's local *isRevealed* boolean variable must be modified upon handling the spacebar key. A simple true/false is not enough, because it's not known whether the screen should just continue showing with the current state, or continue while revealing the card.

For that reason the [HandleStudySession method](StudySession.cs) returs a custom *HandleStudySessionResult* type, which has multiple classes:
```cs
public class HandleStudySessionResult
    {
        public class RevealOrNext : HandleStudySessionResult { }
        public class Exit : HandleStudySessionResult { }
        public class ContinueLoop : HandleStudySessionResult { }
        public class MoveBackward : HandleStudySessionResult { }
    }
```


As said earlier, revealing the onscreen card must be done from the screen scope, so it is handled in [StudySession screen](../screens/StudySession.cs) the following way:
```cs
var handleResult = Logic.HandleStudySession(cardChoiceList);

if (handleResult is Logic.HandleStudySessionResult.RevealOrNext) {
    if (isCardRevealed && (cardChoiceList.selectedIndex != cardChoiceList.MaxIndex)) {
        cardChoiceList.MoveForward();
        isCardRevealed = false;
    }
    else isCardRevealed = true;
}
// rest of status checking
```

And here's how it is returned in the [HandleStudySession](StudySession.cs) method:
```cs
switch (consoleKey)
    {
        // skipped code
        case ConsoleKey.Enter:
        case ConsoleKey.Spacebar:
            return new HandleStudySessionResult.RevealOrNext();
        // skipped code
    }
```

## Actions ##
When an user interactions requires more than just one or two code lines (for example when a dialog needs to be shown and some conditions must be checked), the code should be wrapped into a method of the *Logic* class in the [Actions](../actions/) folder.

Consider the action of renaming a deck. Here's the current code for it, located in [DeckActions](../actions/DeckActions.cs) under the *RenameDeck* method. That'd make the `switch(consoleKey) { ... }` statement unnecessarily long, so it was separated into a method
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
            Deck renamedDeck = RenameDeck(database, deckChoiceList.SelectedItem);
            deckChoiceList.MoveToChoice(renamedDeck);
            break;
    }
```
A RenameDeck call would be enough, but to improve UX the choice list is automatically moved to the newly renamed deck, for example, if due to the sorting it'll change it's position in the deck list.

The deck list utilizes the [ChoiceList](../../CLI/Choicelist.cs) class (in this case the MoveToChoice method), which is generally used for all the horizontal and vertical lists in this app, as a part of that simple CLI library