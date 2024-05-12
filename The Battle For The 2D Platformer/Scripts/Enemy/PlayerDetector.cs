using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event Action PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMover>())
        {
            OnDetected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMover>())
        {
            OnLosted();
        }
    }

    public void OnDetected()
    {
        PlayerDetected?.Invoke();
    }

    public void OnLosted()
    {
        PlayerLost?.Invoke();
    }
}