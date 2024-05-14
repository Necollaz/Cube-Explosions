using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _splitChance = 0.5f;
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

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Cube hitCube))
            {
                List<Cube> newCubes = hitCube.Split(_explosionForce, _splitChance);
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
        Cube newCube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
        newCube.InitializeCube(spawnPosition, Random.ColorHSV(), 0.5f, _splitChance);
        _cubes.Add(newCube);
    }
}
