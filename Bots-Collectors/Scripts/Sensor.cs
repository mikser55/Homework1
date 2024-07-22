using System;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public event Action ResourseFinded;

    [SerializeField] private float _radius;

    public List<Resource> Resources { get; private set; }

    private void Awake()
    {
        Resources = new();
    }

    private void Update()
    {
        FindResourses();
    }

    private void FindResourses()
    {
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, _radius);

        if (nearObjects.Length > 0)
        {
            foreach (var obj in nearObjects)
            {
                obj.TryGetComponent(out Resource resource);

                if (resource != null && !Resources.Contains(resource))
                    Resources.Add(resource);

                if (Resources.Count > 0)
                    ResourseFinded?.Invoke();
            }
        }
    }
}