using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 100f;
    [SerializeField] private float _baseExplosionRadius = 5f;

    public void Explode(Vector3 explosionPosition, float explosionForce, float explosionRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                Vector3 direction = (rigidbody.transform.position - explosionPosition).normalized;
                float distance = Vector3.Distance(explosionPosition, rigidbody.transform.position);
                float force = explosionForce * (1 - (distance / explosionRadius));

                rigidbody.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }

    public float CalculateExplosionForce(Cube cube)
    {
        return _baseExplosionForce / cube.transform.localScale.magnitude;
    }

    public float CalculateExplosionRadius(Cube cube)
    {
        return _baseExplosionRadius * cube.transform.localScale.magnitude;
    }
}