using System;

public class EndGameWindow : Window
{
    public event Action EndButtonClicked;

    protected override void OnButtonClicked()
    {
        EndButtonClicked?.Invoke();
    }
}