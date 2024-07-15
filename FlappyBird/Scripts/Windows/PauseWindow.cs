using System;

public class PauseWindow : Window
{
    public event Action PauseButtonClicked;

    protected override void OnButtonClicked()
    {
        PauseButtonClicked?.Invoke();
    }
}