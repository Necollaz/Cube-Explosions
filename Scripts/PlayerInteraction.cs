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
                if (hit.collider.TryGetComponent(out Cube cube))
                {
                    Vector3 explosionPosition = cube.transform.position;

                    if (Random.value <= cube.SplitChance)
                    {
                        _spawner.SpawnCubes(explosionPosition, cube.SplitChance, cube.transform.localScale);
                    }
                    else
                    {
                        _explosion.Explode(cube);
                    }

                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
