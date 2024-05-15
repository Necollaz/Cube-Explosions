using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private int _minRandomCubes = 2;
    [SerializeField] private int _maxRandomCubes = 7;
    [SerializeField] private float _scaledownFactor = 2f;

    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    public List<Cube> Explode(float explosionForce, float splitChanceDecrease)
    {
        List<Cube> newCubes = new List<Cube>();
        Vector3 center = _cube.transform.position;

        if (Random.value < _cube.SplitChance)
        {
            for (int i = 0; i < Random.Range(_minRandomCubes, _maxRandomCubes); i++)
            {
                Vector3 position = center + Random.insideUnitSphere;
                Color color = Random.ColorHSV();
                float scale = _cube.transform.localScale.x / _scaledownFactor;

                Cube newCube = Instantiate(_cube, position, Quaternion.identity);
                newCube.Initialize(position, color, scale, _cube.SplitChance / splitChanceDecrease);

                Rigidbody newCubeRigidbody = newCube.GetComponent<Rigidbody>();

                if (newCubeRigidbody != null)
                {
                    Vector3 explosionDirection = Random.onUnitSphere;
                    newCubeRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                }

                newCubes.Add(newCube);
            }
        }

        Destroy(_cube.gameObject);

        return newCubes;
    }
}
