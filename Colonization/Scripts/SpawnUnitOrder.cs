using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnitOrder : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _spawnUnitButton;
    [SerializeField] private float _cooldown;

    public event Action SpawnUnitClicked;

    private void OnEnable()
    {
        _spawnUnitButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _spawnUnitButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SpawnUnitClicked?.Invoke();
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        _spawnUnitButton.interactable = false;
        WaitForSeconds wait = new(_cooldown);
        yield return wait;
        _spawnUnitButton.interactable = true;
    }
}