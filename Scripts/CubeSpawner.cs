using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _scaledownFactor = 0.5f;
    [SerializeField] private float _invisibleWallDistance = 5f;

    private Vector3 _initialScale = new Vector3(5f, 5f, 5f);
    private int _maxSpawnCubes = 5;
    private float _splitChanceDivider = 2f;

    private void Start()
    {
        SetupInitialCubes();
    }

    public void SpawnCubes(Vector3 explosionPosition, float parentSplitChance, Vector3 parentScale)
    {
        int numOfCubes = Random.Range(_minCubes, _maxCubes + 1);
        float newSplitChance = parentSplitChance / _splitChanceDivider;
        Vector3 newScale = parentScale * _scaledownFactor;

        SpawnMultipleCubes(numOfCubes, explosionPosition, newScale, newSplitChance);
    }

    private void SetupInitialCubes()
    {
        SpawnMultipleCubes(_maxSpawnCubes, Vector3.zero, _initialScale, 1f);
    }

    private void SpawnMultipleCubes(int numOfCubes, Vector3 basePosition, Vector3 scale, float splitChance)
    {
        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 spawnPosition = basePosition + Random.insideUnitSphere * _invisibleWallDistance;
            spawnPosition.y = Mathf.Max(1f, spawnPosition.y);

            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            newCube.Initialize(spawnPosition, Random.ColorHSV(), scale, splitChance);
        }
    }
}