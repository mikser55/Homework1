using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class ObjectsDetector : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);
            coin.OnCollected();
        }
        else if (collision.gameObject.TryGetComponent(out Kit kit))
        {
            kit.gameObject.SetActive(false);
            _playerStats.Heal(kit.GetHealNumber());
            kit.OnCollected();
        }
    }
}