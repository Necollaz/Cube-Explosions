using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _renderer;

    public float SplitChance { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Vector3 position, Color color, Vector3 scale, float splitChance)
    {
        _rigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        transform.position = position;
        transform.localScale = scale;
        _renderer.material.color = color;
        SplitChance = splitChance;
    }
}