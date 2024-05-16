using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event Action PlayerDetected;
    public event Action PlayerLost;
    public event Action PlayerStayed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMover player))
        {
            OnDetected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMover player))
        {
            OnLosted();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMover player))
        {
            OnStayed();
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

    public void OnStayed()
    {
        PlayerStayed?.Invoke();
    }
}