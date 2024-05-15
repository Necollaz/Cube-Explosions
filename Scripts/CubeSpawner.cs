using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube), typeof(CubeExplosion))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _invisibleWallDistance;
    [SerializeField] private int _maxNumberCubes = 10;
    [SerializeField] private float _scale = 2f;
    [SerializeField] private float _splitChance = 2f;

    private List<Cube> _cubes;
    private Camera _mainCamera;
    private CubeExplosion _explosion;
    private Cube _cube;

    private void Awake()
    {
        _explosion = GetComponent<CubeExplosion>();
        _cube = GetComponent<Cube>();
    }

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
        InteractWithCube();
    }

    private void InteractWithCube()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Cube hitCube))
            {
                _explosion = hitCube.GetComponent<CubeExplosion>();

                if(_explosion != null)
                {
                    List<Cube> newCubes = _explosion.Explode(_explosionForce, _scale);
                    _cubes.Remove(hitCube);
                    _cubes.AddRange(newCubes);
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

        newCube.Initialize(spawnPosition, Random.ColorHSV(), fixedScale, _splitChance);

        _cubes.Add(newCube);
    }
}