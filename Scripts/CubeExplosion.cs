using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _force = 200f;
    [SerializeField] private float _radius = 50f;

    public void Explode(Cube cube)
    {
        Vector3 explosionPosition = cube.transform.position;
        float explosionForce = CalculateForce(cube);
        float explosionRadius = CalculateRadius(cube);

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 1.0f, ForceMode.Impulse);
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