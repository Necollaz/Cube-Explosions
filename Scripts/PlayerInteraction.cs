using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private CubeExplosion cubeExplosion;

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
                        if (Random.value <= cube.SplitChance)
                        {
                            Vector3 explosionPosition = cube.transform.position;
                            Destroy(hit.collider.gameObject);
                            cubeSpawner.SpawnCubes(explosionPosition, cube.SplitChance, cube.transform.localScale);
                            cubeExplosion.Explode(explosionPosition);
                        }
                        else
                        {
                            Destroy(hit.collider.gameObject);
                            cubeExplosion.Explode(cube.transform.position);
                        }
                    }
                }
            }
        }
    }
}
