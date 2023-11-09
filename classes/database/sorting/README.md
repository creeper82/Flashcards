# Sorting #
The `Sorting` class is used to sort a list of cards, based on a selected [`SortType`](SortTypes.cs).

## ApplySort ##
This is an extension method of `IEnumerable<Card>`. A [`SortType`](SortType.cs) enum is passed as first parameter. The returned value is the same list of cards, but sorted.

## Example ##
For example, to sort a list of cards from the oldest to the newest, consider the following code:
```cs
using static Flashcards.Sorting;

// let's say this enumerable contains some cards
IEnumerable<Card> someCards = (...);
IEnumerable<Card> cardsFromOldest = someCards.ApplySort(SortType.DATE_ASCENDING)
```

## Shuffle ##
This is a special method to randomly shuffle cards in an enumerable, and return it later. It's really useful for studying flashcards, while sorting was useful for displaying and managing them.

An example on how to shuffle cards:
```cs
IEnumerable<Card> shuffledCards = someCards.Shuffle();
```
As simple as that...