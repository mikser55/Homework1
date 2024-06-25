using UnityEngine;

[RequireComponent(typeof(Bomb))]
public class BombExplosion : MonoBehaviour
{
    private Bomb _bomb;
    private float _explosionForce = 10f;
    private float _explosionRadius = 50f;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
    }

    private void OnEnable()
    {
        _bomb.BombExploded += Explode;
    }

    private void OnDisable()
    {
        _bomb.BombExploded -= Explode;
    }

    private void Explode()
    {
        float upwardsModifier = 3f;
        Vector3 bombPosition = transform.position;

        Collider[] colliders = Physics.OverlapSphere(bombPosition, _explosionRadius);

        if (colliders.Length > 0)
        {
            foreach (Collider hit in colliders)
            {
                hit.TryGetComponent(out Rigidbody rigidbody);

                if (rigidbody != null)
                {
                    float distance = Vector3.Distance(bombPosition, hit.transform.position);
                    float force = _explosionForce * (1f - (distance / _explosionRadius));
                    rigidbody.AddExplosionForce(force, bombPosition, _explosionRadius, upwardsModifier, ForceMode.Impulse);
                }
            }
        }
    }
}
