namespace CLI;

public static class Dialogs
{
    private static class DialogScreens
    {
        public static void ConfirmScreen(
            string title,
            string message = "",
            string okButton = "OK",
            string cancelButton = "Cancel"
        )
        {
            Console.WriteLine("Simple confirmation screen");
        }
    }
    public static bool Confirm(
        string title,
        string message = "",
        string okButton = "OK",
        string cancelButton = "Cancel"
    )
    {
        DialogScreens.ConfirmScreen(title);
        return false;
    }
}