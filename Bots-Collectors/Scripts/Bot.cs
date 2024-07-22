using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourceSpawner;

    private BotMover _mover;
    private Transform _botTransform;
    private Transform _currentResource;

    public bool IsCarryingResource { get; private set; }
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _botTransform = transform;
    }

    public void DropResourse()
    {
        _currentResource.transform.SetParent(null);
        IsBusy = false;
        IsCarryingResource = false;
        _currentResource.TryGetComponent(out Resource resource);

        if (resource != null)
            _resourceSpawner.ReleaseResource(resource);
    }

    public void SetBusy()
    {
        IsBusy = true;
    }

    public void CollectResourse(Transform target)
    {
        target.transform.SetParent(_botTransform);
        IsCarryingResource = true;
        _mover.MoveToCollectPoint();
    }

    public void TakeCurrentResource(Transform resource)
    {
        _currentResource = resource;
    }
}