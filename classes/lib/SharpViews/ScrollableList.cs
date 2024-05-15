namespace SharpViews;

/// <summary>
/// Used for handling a paginated list you can scroll. Unlike <c>ChoiceList</c>, you can't select any item
/// here. It is only for visual purpose of scrolling, which you can do with <c>MoveForward</c> (scroll down), and
/// <c>MoveBackward</c> (scroll up) methods. Then, use <c>PaginatedChoices</c> to get the current state (visible elements) of the list.
/// You may set the <c>PaginationCount</c> when initialized, to change the maximum number of visible elements at one moment.
/// </summary>
/// <typeparam name="T">Type of the list elements. You can't have mixed types.</typeparam>
/// <param name="choices">List elements.</param>
/// <param name="initialPosition">The initial list position (index). By default, <c>0</c>.</param>
public class ScrollableList<T>(IEnumerable<T> choices, int initialPosition = 0)
{
    /// <summary>
    /// Index of the top-most visible element, based on current scroll position.
    /// </summary>
    public int Position {get; private set;} = initialPosition;

    /// <summary>
    /// Count of elements that are visible at once. By default, <c>9</c>.
    /// </summary>
    /// <remarks>
    /// Use <c>PaginatedChoices</c> to get the list content trimmed to only have such number of elements.
    /// </remarks>
    public int PaginationCount = 9;

    private int MaxAllowedPosition => Math.Max(Choices.Count() - PaginationCount, 0);

    /// <summary>
    /// All the list elements, ignoring scroll position and <c>PaginationCount</c>. You should use <c>PaginatedChoices</c>.
    /// </summary>
    public IEnumerable<T> Choices {get; private set;} = choices;

    /// <summary>
    /// Change the list elements to new ones, and automatically fix the scroll position, if it went out of bounds.
    /// </summary>
    /// <param name="newElements">The new list elements.</param>
    public void UpdateChoices(IEnumerable<T> newElements) {
        Choices = newElements;
        CheckOutOfBoundsPointer();
    }

    /// <summary>
    /// Return the elements that should be visible based on the current scroll position. Change <c>PaginationCount</c> if you want to allow more or less
    /// elements visible.
    /// </summary>
    /// <remarks>
    /// Example: <para>if <c>Choices</c> (list elements) are: <i>apple, banana, cucumber, potato</i>, <c>PaginationCount</c> is <b>3</b>, then:</para>
    /// <para> <c>PaginatedChoices</c> will return: <i>apple, banana, cucumber</i>, and after calling <c>MoveForward</c>:
    /// <i>banana, cucumber, potato</i> </para>
    /// </remarks>
    public IEnumerable<T> PaginatedChoices => Choices.Skip(Position).Take(PaginationCount);

    /// <summary>
    /// Scrolls the list down.
    /// </summary>
    public void MoveForward()
    {
        if (Position < MaxAllowedPosition) Position++;
    }

    /// <summary>
    /// Scrolls the list up.
    /// </summary>
    public void MoveBackward()
    {
        if (Position > 0) Position--;
    }

    /// <summary>
    /// Checks if the pointer (list scroll position) got out of bounds and fix it (snap to the closest element), if so.
    /// It might happen if the list content changes.
    /// </summary>
    /// <remarks>
    /// <b>It is automatically called</b> when moving the list. In most cases you don't need to call it.
    /// </remarks>
    public void CheckOutOfBoundsPointer()
    {
        if (Position > MaxAllowedPosition) Position = MaxAllowedPosition;
        if (Position < 0) Position = 0;
    }
}
