using System;

public class RestartWindow : Window
{
    public event Action RestartButtonClicked;

    protected override void OnButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }
}