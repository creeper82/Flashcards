using Flashcards;

using var db = new FlashcardsContext();

Console.WriteLine($"Database path is {db.DbPath}");