using DG.Tweening;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _scaleDuration = 1f;
    [SerializeField] private float _scaleCofficient = 5f;
    [SerializeField] DoData _data;

    private void Start()
    {
        Scale();
    }

    private void Scale()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(_scaleCofficient, _scaleDuration))
            .SetLoops(-1, _data.LoopType);
    }
}