# CLI #
CLI (command-line interface) is used for rendering all the app screens. The library mainly consists of components, which resemble these from actual GUIs, dialogs, keyboard actions and more...

## ChoiceList ##
The ChoiceList class is used to manage vertical or horizontal item lists consistently around the whole app. Its task is to handle moving the list selection (for example signified by the dot on deck list), and to paginate the choices (also on the deck list, to create the effect of scrolling and not occupy too much space).

### Sample usage ###
Let's consider the usage from the Menu screen, which lists all your decks. The ChoiceList is set up in the [Menu screen](../app/screens/Menu.cs) the following way:
```cs
ChoiceList<Deck> deckChoiceList = new(database.GetDecks())
    {
        PaginationCount = 5
    };
```
That creates a new list with all the decks and sets the pagination count to 5, which means that maximum 5 decks can be seen on the screen at once, to not overflow the screen space in case the user has many decks. The pagination is fully optional

Later, in the Menu screen loop, which is responsible for drawing the UI and handling potential user interactions, we pass the following values to [Screens.Menu](screens/Menu.cs):

- *PaginatedChoices*, which returns the list of items according to the previously set pagination. In case of count 5, it'll only return 5 decks (from deck 1 to deck 5), and as we scroll the list further (i.e. move the selectedIndex), it'll show the next decks, trying to make the currently selected choice appear ideally in the center. For no pagination, you can use *choices*
- *selectedIndex*, which returns the index of currently selected item (used to later show the \[•\] mark on the appropriate deck)
- *PaginationStartIndex*, which returns the first index of the paginated choices in relation to all choices. For example - having 10 decks, when you scroll to deck 5 - thus seeing decks: 3,4,5,6,7 - the PaginationStartIndex will be 2 (because choices are 0-indexed, and the top-positioned deck 3 has index 2). It is needed to be substracted to correctly calculate which list element should have the \[•\] selection mark
```cs
while (running)
        {
            deckChoiceList.CheckOutOfBoundsPointer();
            Screens.Menu(
                deckChoiceList.PaginatedChoices,
                deckChoiceList.selectedIndex,
                deckChoiceList.PaginationStartIndex
            );

            running = Logic.HandleMenu(database, deckChoiceList);
        }
```
### CheckOutOfBoundsPointer ###
At the start of each loop, *CheckOutOfBoundsPointer* is called. Effectively, it checks whether the currently selected index still exists in the list. If not, then move to the closest optimal index. For example having 4 decks, the user removed deck 4. After calling the function, the ChoiceList will move selection to deck 3.

### Moving forward and backward ###
Then, the *HandleMenu* function is called. You can read more about handling [here](../app/handling/), but generally the handler function checks which key have you pressed, and moves the list in case of the up/down arrow key press. All that happens in the [HandleMenu](../app/handling/Menu.cs) file, as follows:

```cs
switch (consoleKey)
        {
            case ConsoleKey.UpArrow:
                deckChoiceList.MoveBackward();
                break;
            case ConsoleKey.DownArrow:
                deckChoiceList.MoveForward();
                break;
            // skipped code (other keys)
        }
```
These methods already have built-in checking whether the list can be moved, and if the list can't be moved (user wants to go forward on the last index and so on), then the call will be ignored

### Moving to specific item ###
You can also move the list to a specific item. This can be useful if the item has changed its position in the list (let's say - the deck was renamed, and now is at the end of the list due to alphabetical sort) and you want to keep the selection on it. Here's what it looks like - again - in the [HandleMenu](../app/handling/Menu.cs) file:
```cs
switch (consoleKey) {
    // skipped code
    case ConsoleKey.R:
    case ConsoleKey.F2:
        Deck renamedDeck = RenameDeck(database, deckChoiceList.SelectedItem);
        deckChoiceList.MoveToChoice(renamedDeck);
        break;
    // skipped code
}
```
After handling the deck rename, the list is moved to the renamed deck even if it changes its index.

## Most common CLI components ##

### UiFrame ###
One of most important components is the *UiFrame*, which is used for essentially every app screen. It takes such arguments:
```cs
string inner
string title = ""
bool horizontalScroll = false
bool verticalScroll = false
```
- inner - the content that should be displayed inside the UiFrame. May be multiline
- title (optional) - the title that should be displayed and centered at the top border of the UiFrame
- horizontalScroll (optional) - whether the horizontal scroll arrows should be shown (to signify scroll possibility)
- verticalScroll (optional) - whether the vertical scroll arrows should be shown (to signify scroll possibility)

Here's an example usage. The static method should be in the *CLI* namespace, for example in *public partial class Screens*:
```cs
public partial class Screens 
{
    internal static void Greeting() 
    {
        ClearConsole();
        Console.WriteLine(
            UiFrame(
                inner: "Have a good day!\n.....",
                title: "Hello",
                verticalScroll: true
            )
        );
    }
}
```
Calling `Screens.Greeting()` will provide the following output:
```
---------------------- Hello ----------------------  

Have a good day!
.....
                                                  ↑
                                                  ↓
--------------------------------------------------- 
```

## Keyboard actions ##
[KeyboardActions](KeyboardAction.cs) are the little tips that signify which keys can be used to interact with the screen. Under `CLI.KeyboardActions` public class, there are pre-coded interactions for every app screen. For example:
```cs
public static List<KeyboardAction> DeckListScreen { get; } = new() {
        new("up/down", "move selection"),
        new("enter", "open deck"),
        new("del", "delete deck"),
        new("r", "rename deck"),
        KeyboardAction.LineSeparator,
        new("n", "create new deck"),
        new("h", "open help")
    };
```
Example output when displayed with *KeyboardActionList*:
```
[ up/down ] - move selection
[ enter ] - open deck
[ del ] - delete deck
[ r ] - rename deck

[ n ] - create new deck
[ h ] - open help
```
The line separator is essentially just `new("", "")`. To display the list of keyboard actions, use the *KeyboardActionList* component, which accepts the list as a parameter. The keyboard action list should be rendered separately from the whole UiFrame. Take a look at the *SortTypePicker* screen:
```cs
internal static void SortTypePicker(List<string> sortTypes, int selectedIndex)
    {
        ClearConsole();
        Console.WriteLine(
            UiFrame(
                inner: List(sortTypes, selectedIndex),
                title: "Sort by",
                verticalScroll: true
            )
        );
        // render the keyboard action list
        Console.WriteLine(KeyboardActionList(KeyboardActions.SortPickerScreen));
    }
```
It's as simple as that. Render the screen, render the keyboard actions and later handle user input (which is not done in provided example function, as it is in `CLI/screens` and not in `app/screens`. CLI screens only display the information)

For handling the user input and more info about screens, see the [app/screens](../app/screens/)