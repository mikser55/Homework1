using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeData _data;

    private CubePoolManager _poolManager;
    private Renderer _renderer;
    private bool _isCollided;
    private Coroutine _liveCoroutine;
    private BombSpawner _bombSpawner;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && collision.gameObject.TryGetComponent(out Ground _))
        {
            _isCollided = true;
            _renderer.material.color = new Color(Random.value, Random.value, Random.value);
            _liveCoroutine = StartCoroutine(LiveTimeCoroutine());
        }
    }

    public void TakeObjectPool(CubePoolManager pool)
    {
        _poolManager = pool;
    }

    public void Init(Color color)
    {
        _isCollided = false;
        _renderer.material.color = color;
    }

    private IEnumerator LiveTimeCoroutine()
    {
        float liveTime = Random.Range(_data.MinCubeLiveTime, _data.MaxCubeLiveTime);

        yield return new WaitForSeconds(liveTime);

        _poolManager.ReturnObject(this);
        StopCoroutine(_liveCoroutine);
    }
}