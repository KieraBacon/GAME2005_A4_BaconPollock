using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CollisionType
{
    Cube,
    Sphere
}

[System.Serializable]
public class PhysicsBehaviour : MonoBehaviour
{
    public CollisionType type;
    public Vector3 acceleration;
    public Vector3 velocity;
    public bool isColliding;
    public List<PhysicsBehaviour> contacts;
    public bool GravityEnabled;
    public bool debug;

    private MeshFilter meshFilter;
    private Bounds bounds;
    public Vector3 max;
    public Vector3 min;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        bounds = meshFilter.mesh.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;

        _Accelerate();
        _Move();
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            switch (type)
            {
                case CollisionType.Cube:
                    Gizmos.DrawWireCube(transform.position, transform.localScale);
                    break;
                case CollisionType.Sphere:
                    Gizmos.DrawWireSphere(transform.position, transform.localScale.x * 0.5f);
                    break;
            }
        }
    }

    private void _Accelerate()
    {
        velocity += acceleration * Time.deltaTime;
    }

    private void _Move()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
