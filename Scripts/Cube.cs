using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;

    public float SplitChance { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Vector3 position, Color color, Vector3 scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = scale;
        _renderer.material.color = color;
        SplitChance = splitChance;
    }
}