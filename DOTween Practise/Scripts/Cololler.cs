using DG.Tweening;
using UnityEngine;

public class Cololler : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Color _color;
    [SerializeField] float _duration;
    [SerializeField] private LoopType _loopType;

    private void Start()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        DOTween.Sequence()
            .Append(_renderer.material.DOColor(_color, _duration))
            .SetLoops(-1, _loopType);
    }
}