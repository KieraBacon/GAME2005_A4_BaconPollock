using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public PhysicsBehaviour[] actors;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        actors = FindObjectsOfType<PhysicsBehaviour>();

        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    CheckCollision(actors[i], actors[j]);
                }
            }
        }
    }
    public static bool HasIntersection(AABB a, AABB b)
    {
        return (a.mMin.x <= b.mMax.x && a.mMax.x >= b.mMin.x) &&
               (a.mMin.y <= b.mMax.y && a.mMax.y >= b.mMin.y) &&
               (a.mMin.z <= b.mMax.z && a.mMax.z >= b.mMin.z);
    }

    public static bool HasIntersection(Sphere a, Sphere b)
    {
        float distSq = (a.mCentre - b.mCentre).sqrMagnitude;
        float sumRadii = a.mRadius + b.mRadius;
        return distSq <= (sumRadii * sumRadii);
    }

    public static bool HasIntersection(Sphere s, AABB b)
    {
        float distSq = b.MinDistSq(s.mCentre);
        Debug.Log(distSq);
        return distSq <= (s.mRadius * s.mRadius);
    }

    public static bool HasIntersection(PhysicsBehaviour a, PhysicsBehaviour b)
    {


        if (a.type == CollisionType.Cube)
        {
           // Debug.Log("a.tyoe == Cube");
            if (b.type == CollisionType.Cube)
            {
                //Debug.Log("b.tyoe == Cube");
                return HasIntersection(a.aabb, b.aabb);
            }
            else if (b.type == CollisionType.Sphere)
            {
                Debug.Log("b.tyoe == sphere");
                return HasIntersection(b.sphere, a.aabb);
            }
        }
        else if (a.type == CollisionType.Sphere)
        {
            Debug.Log("a.tyoe == Sphere");
            if (b.type == CollisionType.Cube)
            {
                //Debug.Log("b.tyoe == Cube");
                return HasIntersection(a.sphere, b.aabb);
            }
            else if (b.type == CollisionType.Sphere)
            {
                Debug.Log("b.tyoe == sphere");
                return HasIntersection(a.sphere, b.sphere);
            }
        }
        return false;
    }

    public static void CheckCollision(PhysicsBehaviour a, PhysicsBehaviour b)
    {
        if (HasIntersection(a, b))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
            }

            if (!b.contacts.Contains(a))
            {
                b.contacts.Add(a);
                b.isColliding = true;
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }

            if (b.contacts.Contains(a))
            {
                b.contacts.Remove(a);
                b.isColliding = false;
            }
        }
    }
}
