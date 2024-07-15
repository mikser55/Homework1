using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _actionButton;

    protected CanvasGroup WindowGroup => _windowGroup;   
    protected Button ActionButton => _actionButton;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClicked);
    }

    public virtual void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    public virtual void Open()
    {
        WindowGroup.alpha = 1f;
        ActionButton.interactable = true;
    }

    protected abstract void OnButtonClicked();
}