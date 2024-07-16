using DG.Tweening;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _zRotateDegrees = 180f;
    [SerializeField] private float _rotateDuration = 1f;
    [SerializeField] DoData _data;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0f, 0f, _zRotateDegrees), _rotateDuration))
            .SetLoops(-1, _data.LoopType);
    }
}
