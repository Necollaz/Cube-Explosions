using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _scaledownFactor = 0.5f;
    [SerializeField] private float _invisibleWallDistance = 5f;

    private List<Cube> _cubes;
    private Vector3 _initialScale = new Vector3(5f, 5f, 5f);
    private int _maxSpawnCubes = 5;

    private void Start()
    {
        SetupInitialCubes();
    }

    public void SpawnCubes(Vector3 explosionPosition, float parentSplitChance, Vector3 parentScale)
    {
        int numOfCubes = Random.Range(_minCubes, _maxCubes + 1);
        float newSplitChance = parentSplitChance / 2f;
        Vector3 newScale = parentScale * _scaledownFactor;

        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 spawnPosition = explosionPosition + Random.insideUnitSphere * _invisibleWallDistance;
            spawnPosition.y = Mathf.Max(1f, spawnPosition.y);
            Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
            newCube.Initialize(spawnPosition, Random.ColorHSV(), newScale, newSplitChance);
            _cubes.Add(newCube);
        }
    }

    private void SetupInitialCubes()
    {
        _cubes = new List<Cube>();

        for (int i = 0; i < _maxSpawnCubes; i++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * _invisibleWallDistance;
            spawnPosition.y = Mathf.Max(1f, spawnPosition.y);

            Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
            newCube.Initialize(spawnPosition, Random.ColorHSV(), _initialScale, 1f);
            _cubes.Add(newCube);
        }
    }
}
