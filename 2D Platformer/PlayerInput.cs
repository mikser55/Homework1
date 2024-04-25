using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalDirection = "Horizontal";
    private string _jump = "Jump";

    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw(_horizontalDirection);
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown(_jump);
    }
}