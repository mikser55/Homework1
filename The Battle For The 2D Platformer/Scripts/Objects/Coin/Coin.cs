using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action CoinCollected;

    public void OnCollected()
    {
        CoinCollected?.Invoke();
    }
}