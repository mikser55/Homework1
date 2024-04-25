using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);
            _coin.OnCollected();
        }
    }
}