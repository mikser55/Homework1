using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public event Action ScoreChanged;
    public event Action Died;

    public int Score { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
            if (collision.gameObject.TryGetComponent(out Ground _))
                Die();
    }

    public void IncreaseScoreNumber()
    {
        Score++;
        ScoreChanged?.Invoke();
    }

    public void Die()
    {
        Destroy(this.gameObject);
        Died?.Invoke();
    }
}