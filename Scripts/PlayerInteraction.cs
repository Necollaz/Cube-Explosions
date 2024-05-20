using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExplosion _cubeExplosion;

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
                if (hit.collider.CompareTag("Cube"))
                {
                    Cube cube = hit.collider.GetComponent<Cube>();

                    if (cube != null)
                    {
                        Vector3 explosionPosition = cube.transform.position;
                        float explosionForce = _cubeExplosion.CalculateExplosionForce(cube);
                        float explosionRadius = _cubeExplosion.CalculateExplosionRadius(cube);

                        if (Random.value <= cube.SplitChance)
                        {
                            Destroy(hit.collider.gameObject);
                            _cubeSpawner.SpawnCubes(explosionPosition, cube.SplitChance, cube.transform.localScale);
                        }
                        else
                        {
                            Destroy(hit.collider.gameObject);
                            _cubeExplosion.Explode(explosionPosition, explosionForce, explosionRadius);
                        }
                    }
                }
            }
        }
    }
}
