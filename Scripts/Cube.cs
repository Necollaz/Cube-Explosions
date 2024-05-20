using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private Rigidbody _cubeRigidbody;
    private MeshRenderer _cubeRenderer;

    public float SplitChance { get; private set; }

    private void Awake()
    {
        _cubeRigidbody = GetComponent<Rigidbody>();
        _cubeRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Vector3 position, Color color, Vector3 scale, float splitChance)
    {
        if (TryGetComponent(out _cubeRigidbody))
        {
            _cubeRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        }

        transform.position = position;
        transform.localScale = scale;
        _cubeRenderer.material.color = color;
        SplitChance = splitChance;
    }
}