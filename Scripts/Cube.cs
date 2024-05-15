using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }

    private Rigidbody _cubeRigidbody;
    private Renderer _cubeRenderer;

    private void Awake()
    {
        _cubeRigidbody = GetComponent<Rigidbody>();
        _cubeRenderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 position, Color color, float scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        _cubeRenderer.material.color = color;
        SplitChance = splitChance;

        if (_cubeRigidbody != null)
        {
            _cubeRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
    }
}