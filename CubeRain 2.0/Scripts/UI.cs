using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private BombPoolManager _bombPool;
    [SerializeField] private CubePoolManager _cubePool;
    [SerializeField] private TextMeshProUGUI _allObjectsText;
    [SerializeField] private TextMeshProUGUI _bombCountText;
    [SerializeField] private TextMeshProUGUI _cubeCountText;
    [SerializeField] private TextMeshProUGUI _activeObjects;

    private void OnEnable()
    {
        _bombPool.ObjectsCountChanged += UpdateText;
        _cubePool.ObjectsCountChanged += UpdateText;
    }

    private void OnDisable()
    {
        _bombPool.ObjectsCountChanged -= UpdateText;
        _cubePool.ObjectsCountChanged -= UpdateText;
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        var allActiveObjects = _cubePool.GetNumberActiveObjects() + _bombPool.GetNumberActiveObjects();
        var allObjects = _bombPool.AllCreatedObjects + _cubePool.AllCreatedObjects;

        _allObjectsText.text = "Общее количество объектов: " + allObjects.ToString();
        _bombCountText.text = "Количество бомб: " + _bombPool.AllCreatedObjects.ToString();
        _cubeCountText.text = "Количество кубов: " + _cubePool.AllCreatedObjects.ToString();
        _activeObjects.text = "Количество активных объектов: " + allActiveObjects.ToString();
    }
}