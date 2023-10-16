namespace CLI;

public class ChoiceList<T>
{
    public int selectedIndex = 0;
    public int PaginationCount = 10;

    public T? SelectedItem
    {
        get
        {
            return choices.Any() ? choices.Skip(selectedIndex).Take(1).FirstOrDefault() : default;
        }
    }

    public int MaxIndex
    {
        get
        {
            return choices.Count() - 1;
        }
    }

    public int PaginationStartIndex
    {
        get
        {
            if (selectedIndex >= MaxIndex - (PaginationCount / 2)) return Math.Max(0, MaxIndex - PaginationCount + 1);
            return Math.Max(0, selectedIndex - (PaginationCount / 2));
        }
    }

    public IEnumerable<T> choices;

    public IEnumerable<T> PaginatedChoices
    {
        get
        {
            return choices.Skip(PaginationStartIndex).Take(PaginationCount);
        }
    }

    public ChoiceList(IEnumerable<T> choices)
    {
        this.choices = choices;
    }

    public void MoveForward()
    {
        if (selectedIndex < MaxIndex) selectedIndex++;
    }

    public void MoveBackward()
    {
        if (selectedIndex > 0) selectedIndex--;
    }

    public void CheckOutOfBoundsPointer()
    {
        if (selectedIndex > MaxIndex) selectedIndex = MaxIndex;
        if (selectedIndex < 0) selectedIndex = 0;
    }

    public void MoveToChoice(T item)
    {
        selectedIndex = choices.ToList().IndexOf(item);
    }
}
