namespace Flashcards;

public class Database {
    public FlashcardsContext db = new();
    public string Path = "";

    public Database() {
        Path = db.DbPath;
    }
}