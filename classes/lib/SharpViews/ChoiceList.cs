namespace SharpViews;

/// <summary>
/// Used for handling a list of choices, in which you can select an item and scroll. Scrolling is done with <c>MoveForward</c> and <c>MoveBackward</c>
/// methods. Then, use <c>PaginatedChoices</c> to get the current state (visible elements) of the list.
/// You may set the <c>PaginationCount</c> when initialized, to change the maximum number of visible elements at one moment.
/// </summary>
/// <typeparam name="T">Type of the list elements. You can't have mixed types. Only reference types allowed (e.g. <c>string</c>, <c>class</c>,
/// but not <c>int</c>), due to the null design under the hood.</typeparam>
/// <param name="choices">List elements.</param>
/// <param name="initialIndex">The initially selected index. By default <c>0</c>.</param>
/// <remarks>
/// <para>When selecting items is not needed, use <c>ScrollableList</c>. Almost identical behavior, but no selection.</para>
/// <para>Ideally, display the ChoiceList using <c>Components.List</c>. Put a list of strings, <c>SelectedIndex</c> and <c>PaginationStartIndex</c> into the
/// <c>Components.List</c>'s arguments. The first argument must be a string list, so if your choice list consists of specific objects, you should
/// for example cast their names into an array. e.g. with LINQ: <c>choices.Select(c => c.Name)</c></para>
/// </remarks>
public class ChoiceList<T>(IEnumerable<T> choices, int initialIndex = 0) where T : class
{
    /// <summary>
    /// Currently selected index.
    /// </summary>
    public int SelectedIndex {get; private set;} = initialIndex;

    /// <summary>
    /// Count of visible elements at once. By default, <c>9</c>.
    /// </summary>
    /// <remarks>
    /// Use <c>PaginatedChoices</c> to get the list content trimmed to only have such number of elements.
    /// </remarks>
    public int PaginationCount = 5;

    /// <summary>
    /// Currently selected list element (not index). It should be fast, but try to use it only when the user takes action with an item,
    /// and not on every scroll.
    /// </summary>
    public T? SelectedChoice =>
        Choices.Any() ? Choices.Skip(SelectedIndex).Take(1).FirstOrDefault() : null;
    
    /// <summary>
    /// Largest index in the list.
    /// </summary>
    public int MaxIndex => Math.Max(Choices.Count() - 1, 0);

    /// <summary>
    /// Index of the top-most visible element, based on current scroll position.
    /// Can be passed into <c>Components.List</c>'s <c>startIndex</c> argument.
    /// </summary>
    public int PaginationStartIndex
    {
        get
        {
            if (SelectedIndex >= MaxIndex - (PaginationCount / 2)) return Math.Max(0, MaxIndex - PaginationCount + 1);
            return Math.Max(0, SelectedIndex - (PaginationCount / 2));
        }
    }

    /// <summary>
    /// Change the list choices to new ones, and automatically fix the selection mark, if the selection went out of bounds.
    /// </summary>
    /// <param name="newChoices">The new list of choices.</param>
    public void UpdateChoices(IEnumerable<T> newChoices) {
        Choices = newChoices;
        CheckOutOfBoundsPointer();
    }

    /// <summary>
    /// All the list elements, ignoring scroll position and <c>PaginationCount</c>. You should use <c>PaginatedChoices</c> to get the visible choices,
    /// and <c>UpdateChoices()</c> to set new choices.
    /// </summary>
    public IEnumerable<T> Choices {get; private set;} = choices;

    /// <summary>
    /// Return the elements that should be visible based on the current scroll position. Change <c>PaginationCount</c> if you want to allow more or less
    /// elements visible.
    /// </summary>
    /// <remarks>
    /// Unlike <c>ScrollableList</c>, the scrolling behavior is selected item-centered, i.e. the selected item will be in the center, and not at the top.
    /// Idk how to explain it. You will see when you try it out.
    /// </remarks>
    public IEnumerable<T> PaginatedChoices => Choices.Skip(PaginationStartIndex).Take(PaginationCount);
    
    /// <summary>
    /// Scrolls the list down.
    /// </summary>
    public void MoveForward()
    {
        if (SelectedIndex < MaxIndex) SelectedIndex++;
    }

    /// <summary>
    /// Scrolls the list up.
    /// </summary>
    public void MoveBackward()
    {
        if (SelectedIndex > 0) SelectedIndex--;
    }

    /// <summary>
    /// Checks if the pointer (list scroll position) got out of bounds and fix it (snap to the closest element), if so.
    /// It might happen if the list content changes.
    /// </summary>
    /// <remarks>
    /// <b>It is automatically called</b> when moving the list. In most cases you don't need to call it.
    /// </remarks>
    private void CheckOutOfBoundsPointer()
    {
        if (SelectedIndex > MaxIndex) SelectedIndex = MaxIndex;
        if (SelectedIndex < 0) SelectedIndex = 0;
    }

    /// <summary>
    /// Instantly scrolls the list to given element, if it exists in the choice list.
    /// </summary>
    /// <param name="item">Element to scroll to.</param>
    public void MoveToChoice(T item)
    {
        int index = Choices.ToList().IndexOf(item);
        if (index != -1) {
            SelectedIndex = Choices.ToList().IndexOf(item);
            CheckOutOfBoundsPointer();
        }
    }
}
