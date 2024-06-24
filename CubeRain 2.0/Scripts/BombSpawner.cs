using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private BombPoolManager _poolManager;

    public void Spawn(Vector3 position)
    {
        Bomb bomb = _poolManager.GetBomb();
        bomb.transform.position = position;
    }
}