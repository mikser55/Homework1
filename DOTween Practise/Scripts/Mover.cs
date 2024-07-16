using DG.Tweening;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] DoData _data;
    [SerializeField] private float _zDistance = 10f;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z + _zDistance), _data.Duration))
            .SetLoops(-1, _data.LoopType);
    }
}
