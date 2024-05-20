using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 5f;

    public void Explode(Vector3 explosionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        foreach (Collider cubeCollider in colliders)
        {
            if (cubeCollider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}