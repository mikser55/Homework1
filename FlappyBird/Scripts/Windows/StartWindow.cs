using System;

public class StartWindow : Window
{
    public event Action StartButtonClicked;

    protected override void OnButtonClicked()
    {
        StartButtonClicked?.Invoke();
    }
}