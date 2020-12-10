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
public class CollisionBehaviour : MonoBehaviour
{
    public CollisionType type;
    public Vector3 acceleration;
    public Vector3 velocity;
    public bool isColliding;
    public List<CollisionBehaviour> contacts;
    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Accelerate();
        _Move();
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            switch(type)
            {
                case CollisionType.Cube:
                    Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
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
