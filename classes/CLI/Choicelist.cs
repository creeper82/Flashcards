namespace CLI;

public class ChoiceList<T>
{
    public int selectedIndex = 0;

    public T SelectedItem
    {
        get
        {
            return choices.ToList()[selectedIndex];
        }
    }

    public int MaxIndex
    {
        get
        {
            return choices.Count() - 1;
        }
    }
    public IEnumerable<T> choices;

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
