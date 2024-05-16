using UnityEngine;

[RequireComponent(typeof(Health))]
public class ObjectsDetector : MonoBehaviour
{
    [SerializeField] private KitData _kitData;

    private Health _playerStats;

    private void Awake()
    {
        _playerStats = GetComponent<Health>();
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
            _playerStats.Heal(_kitData._kitHealNumber);
            kit.OnCollected();
        }
    }
}