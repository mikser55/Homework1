using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;

    private float _horizontalInput;

    private void Update()
    {
        UpdateInput();
        ChangeMovementDirection();
    }

    private void UpdateInput()
    {
        _horizontalInput = _playerMover.GetHorizontalInput();
    }

    private void ChangeMovementDirection()
    {
        if (_horizontalInput > 0)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (_horizontalInput < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}
