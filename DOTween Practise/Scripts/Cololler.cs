using DG.Tweening;
using UnityEngine;

public class Cololler : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Color _color;
    [SerializeField] DoData _data;

    private void Start()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        DOTween.Sequence()
            .Append(_renderer.material.DOColor(_color, _data.Duration))
            .SetLoops(-1, _data.LoopType);
    }
}