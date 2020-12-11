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
    public bool showDebugGizmo;

    [Header("Motion")]
    public Vector3 acceleration;
    public Vector3 velocity;
    public bool GravityEnabled;

    [Header("Collision")]
    public bool mobile;
    public CollisionType type;
    public float mass;
    public float bounciness;
    public float friction;

    [Header("Internal")]
    [HideInInspector] public bool isColliding;
    [HideInInspector] public List<PhysicsBehaviour> contacts;
    [HideInInspector] private MeshFilter meshFilter;
    [HideInInspector] public Bounds bounds;
    [HideInInspector] public AABB aabb;
    [HideInInspector] public Sphere sphere;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        bounds = meshFilter.mesh.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case CollisionType.Cube:
                aabb.mMax = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
                aabb.mMin = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
                break;
            case CollisionType.Sphere:
                sphere.mCentre = transform.position;
                sphere.mRadius = transform.localScale.x * 0.5f;
                break;
        }

        _Accelerate();
        _Move();
    }

    private void OnDrawGizmos()
    {
        if (showDebugGizmo)
        {
            Gizmos.color = Color.magenta;
            if(isColliding)
                Gizmos.color = Color.cyan;

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
        if (mobile)
            velocity += acceleration * Time.deltaTime;
    }

    private void _Move()
    {
        if(mobile)
            transform.position += velocity * Time.deltaTime;
    }
}
