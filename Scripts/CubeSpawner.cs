using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _splitChance = 0.5f;
    [SerializeField] private float _invisibleWallDistance;
    [SerializeField] private int _maxNumberCubes = 10;
    [SerializeField] private float _scale = 2f;

    private List<Cube> _cubes;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
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
                List<Cube> newCubes = hitCube.Split(_explosionForce, _scale);

                foreach (Cube cube in newCubes)
                {
                    cube.InitializeCube(hitCube.transform.position, Random.ColorHSV(), cube.transform.localScale.x, _splitChance / _scale);
                }
            }
        }
    }

    private void SpawnRandomCube()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * _invisibleWallDistance;
        spawnPosition.y = Mathf.Max(1f, spawnPosition.y);

        Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

        float fixedScale = 3f;
        newCube.transform.localScale = new Vector3(fixedScale, fixedScale, fixedScale);

        newCube.InitializeCube(spawnPosition, Random.ColorHSV(), fixedScale, _splitChance);

        _cubes.Add(newCube);

    }
}