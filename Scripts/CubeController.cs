using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeController : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _splitChance;
    [SerializeField] private float _invisibleWallDistance;

    private List<Cube> _cubes;
    private int _maxNumberCubes = 10;

    private void Start()
    {
        _cubes = new List<Cube>();

        for (int i = 0; i < _maxNumberCubes; i++)
        {
            SpawnRandomCube();
        }
    }

    private void Update()
    {
        InteractionCube();
    }

    private void InteractionCube()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<Cube>() != null)
            {
                Cube hitCube = hit.collider.GetComponent<Cube>();
                List<Cube> newCubes = SplitCube(hitCube);
                _cubes.Remove(hitCube);
                foreach (Cube cube in newCubes)
                {
                    _cubes.Add(cube);
                }
                Destroy(hitCube.gameObject);
            }
        }
    }

    private void SpawnRandomCube()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * _invisibleWallDistance;
        spawnPosition.y = Mathf.Max(1f, spawnPosition.y);
        Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
        newCube.transform.localScale /= 2f;
        newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
        Rigidbody newCubeRigidbody = newCube.GetComponent<Rigidbody>();

        if (newCubeRigidbody != null)
        {
            newCubeRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        }

        _cubes.Add(newCube);
    }

    private List<Cube> SplitCube(Cube cubeToSplit)
    {
        List<Cube> newCubes = new List<Cube>();
        Vector3 center = cubeToSplit.transform.position;

        for (int i = 0; i < Random.Range(2, 7); i++)
        {
            Cube newCube = Instantiate(_cubePrefab, center, Quaternion.identity);
            newCube.transform.localScale = cubeToSplit.transform.localScale / 2f;
            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            Rigidbody newCubeRigidbody = newCube.GetComponent<Rigidbody>();

            if (newCubeRigidbody != null)
            {
                if(Random.Range(0f, 1f) < _splitChance)
                {
                    Vector3 direction = (newCube.transform.position - center).normalized;
                    newCubeRigidbody.AddForce(direction * _explosionForce, ForceMode.Impulse);
                    _splitChance /= 2f;
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
