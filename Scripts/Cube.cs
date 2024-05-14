using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private int _minRandomCubes = 2;
    private int _maxRandomCube = 7;
    private float _scaledownFactor = 2f;
    private float _splitChance;

    public void InitializeCube(Vector3 position, Color color, float scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        GetComponent<Renderer>().material.color = color;
        _splitChance = splitChance;

        Rigidbody cubeRigidbody = GetComponent<Rigidbody>();

        if (cubeRigidbody != null)
        {
            cubeRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
    }

    public List<Cube> Split(float explosionForce, float splitChanceDecrease)
    {
        List<Cube> newCubes = new List<Cube>();
        Vector3 center = transform.position;

        if(Random.value < _splitChance)
        {
            for (int i = 0; i < Random.Range(_minRandomCubes, _maxRandomCube); i++)
            {
                Cube newCube = Instantiate(this, center, Quaternion.identity);
                newCube.transform.localScale = transform.localScale / _scaledownFactor;
                newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
                newCube.InitializeCube(center, Random.ColorHSV(), newCube.transform.localScale.x, _splitChance / splitChanceDecrease);

                Rigidbody newCubeRigidbody;

                if(newCube.TryGetComponent(out newCubeRigidbody))
                {
                    Vector3 explosionDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                    newCubeRigidbody.AddForce(explosionDirection.normalized * explosionForce, ForceMode.Impulse);
                }

                newCubes.Add(newCube);
            }
        }

        Destroy(gameObject);

        return newCubes;
    }
}