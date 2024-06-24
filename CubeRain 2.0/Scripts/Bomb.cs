using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    public event Action BombExploded;

    private float _targetAlpha = 0f;
    private float _maxAlpha = 1f;
    private float _currentAlpha = 1f;
    private Renderer _renderer;
    private BombPoolManager _poolManager;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _currentAlpha = _maxAlpha;
        SetTransparency(_maxAlpha);
        StartCoroutine(LiveTimeCoroutine());
    }

    public void TakeObjectPool(BombPoolManager poolManager)
    {
        _poolManager = poolManager;
    }

    private void OnBombExploded()
    {
        BombExploded?.Invoke();
    }

    private void SetTransparency(float value)
    {
        Color color = _renderer.material.color;
        color.a = Mathf.Clamp01(value);
        _renderer.material.color = color;
    }

    private IEnumerator LiveTimeCoroutine()
    {
        int _minDelay = 2;
        int _maxDelay = 6;
        int randomLiveTime = Random.Range(_minDelay, _maxDelay);

        while (_currentAlpha > _targetAlpha)
        {
            _currentAlpha = Mathf.MoveTowards(_currentAlpha, _targetAlpha, Time.deltaTime / randomLiveTime);
            SetTransparency(_currentAlpha);
            yield return null;
        }

        OnBombExploded();
        _poolManager.ReturnBomb(this);
    }
}