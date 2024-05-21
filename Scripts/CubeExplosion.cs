using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _force = 200f;
    [SerializeField] private float _radius = 50f;

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

    public float CalculateForce(Cube cube)
    {
        return _force / cube.transform.localScale.magnitude;
    }

    public float CalculateRadius(Cube cube)
    {
        return _radius * cube.transform.localScale.magnitude;
    }
}