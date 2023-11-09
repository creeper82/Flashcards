# Filtering #
All filtering-related objects are in the `Filtering` class. The main purpose is to filter out a list of cards, based on [`CardFilter`](CardFilter.cs) class settings.

## CardFilter ##
This class contains all the filters that should be applied to a card list. For example, to show only cards from 5 days, you may create a following filter:
```cs
using static Flashcards.Filtering;

CardFilter example = new CardFilter(
    recentDays: 5
);

```
Later on, the filter can be applied with [`ApplyFilter`](ApplyFilter.cs) extension method, for example:
```cs
using static Flashcards.Filtering;

CardFilter example = new CardFilter(
    recentDays: 5
);

//some list of flashcards
IEnumerable<Card> someCards = (...);
IEnumerable<Card> filteredCards = someCards.ApplyFilter(example);
```

## KeywordMatchMode ##
When searching for a specific keyword in cards, you may use one of the following search modes:
- any (anywhere in the card)
- front (only search card fronts)
- back (only search card backs)

Assuming you want to search "1838" only on card back sides, you may do it the following way:
```cs
using static Flashcards.Filtering;

CardFilter searchFilter = new CardFilter(
    keyword: "1838",
    matchMode: KeywordMatchMode.CardBack
);

IEnumerable<Card> searchedCards = someCards.ApplyFilter(searchFilter);
```

## CardFilterPicker ##
There exists a [CardFilterPicker](../../app/screens/CardFilterPicker.cs) screen (read more about screens [here](../../app/screens/)), which allows user to pick filters using a graphical interface. You can see it in action, when you press \[F\], while browsing deck's cards.