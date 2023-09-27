namespace CLI;

public static class Options {
    public static List<CLI.Option> MenuOptions {get;} = new() {
        new("up/down", "move selection")
    };
}