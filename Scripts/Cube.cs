using System.Collections.Generic;
using UnityEngine;

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

    public List<Cube> Split(float explosionForce, float splitChance)
    {
        List<Cube> newCubes = new List<Cube>();
        Vector3 center = transform.position;

        for (int i = 0; i < Random.Range(_minRandomCubes, _maxRandomCube); i++)
        {
            Cube newCube = Instantiate(this, center, Quaternion.identity);
            newCube.transform.localScale = transform.localScale / _scaledownFactor;
            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            newCube.InitializeCube(center, Random.ColorHSV(), newCube.transform.localScale.x, _splitChance / _scaledownFactor);
            Rigidbody newCubeRigidbody = newCube.GetComponent<Rigidbody>();

            if (newCubeRigidbody != null)
            {
                if (Random.Range(0f, 1f) < splitChance)
                {
                    Vector3 direction = (newCube.transform.position - center).normalized;
                    newCubeRigidbody.AddForce(direction * explosionForce, ForceMode.Impulse);
                    splitChance /= _scaledownFactor;
                }
                else
                {
                    Destroy(newCube.gameObject);
                }
            }

            newCubes.Add(newCube);
        }

        return newCubes;
    }
}