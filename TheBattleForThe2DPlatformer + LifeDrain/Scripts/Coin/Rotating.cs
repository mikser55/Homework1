using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private CoinSpawner _spawner;
    private CoinData _coinData;

    private void Awake()
    {
        _coinData = _spawner.GetData();
    }

    private void Update()
    {
        transform.Rotate(0f, _coinData.CoinRotateSpeed * Time.deltaTime, 0f);
    }
}