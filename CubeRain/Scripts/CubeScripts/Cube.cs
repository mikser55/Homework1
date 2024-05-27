using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeData _cubeData;

    private ObjectPoolManager _poolManager;
    private Renderer _renderer;
    private bool _isColorChanged;
    private Coroutine _liveCoroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == false && collision.gameObject.TryGetComponent(out Ground _))
        {
            _isColorChanged = true;
            _renderer.material.color = new Color(Random.value, Random.value, Random.value);
            _liveCoroutine = StartCoroutine(LiveTimeCoroutine());
        }
    }

    private IEnumerator LiveTimeCoroutine()
    {
        float liveTime = Random.Range(_cubeData.MinCubeLiveTime, _cubeData.MaxCubeLiveTime);
        yield return new WaitForSeconds(liveTime);

        _poolManager.ReturnCube(this);
        StopCoroutine(_liveCoroutine);
    }

    public void TakeObjectPool(ObjectPoolManager pool)
    {
        _poolManager = pool;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetStartColorStatus()
    {
        _isColorChanged = false;
    }
}