using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExplosion _explosion;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        InteractWithCube();
    }

    private void InteractWithCube()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Cube>(out Cube cube))
                {
                    Vector3 explosionPosition = cube.transform.position;
                    float explosionForce = _explosion.CalculateForce(cube);
                    float explosionRadius = _explosion.CalculateRadius(cube);

                    if (Random.value <= cube.SplitChance)
                    {
                        _spawner.SpawnCubes(explosionPosition, cube.SplitChance, cube.transform.localScale);
                    }
                    else
                    {
                        _explosion.Explode(explosionPosition, explosionForce, explosionRadius);
                    }

                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
