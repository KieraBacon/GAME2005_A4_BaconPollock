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
    void FixedUpdate()
    {
        actors = FindObjectsOfType<PhysicsBehaviour>();

        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    if(actors[i].contacts.Count == 0)
                        actors[i].isColliding = false;
                    CheckCollision(actors[i], actors[j]);
                }
            }
        }

        for (uint i = 0; i < actors.Length; i++)
        {
            ResolveCollisions(actors[i]);
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
        return distSq <= (s.mRadius * s.mRadius);
    }

    public static bool HasIntersection(PhysicsBehaviour a, PhysicsBehaviour b)
    {
        if (a.type == CollisionType.Cube)
        {
            if (b.type == CollisionType.Cube)
            {
                return HasIntersection(a.aabb, b.aabb);
            }
            else if (b.type == CollisionType.Sphere)
            {
                return HasIntersection(b.sphere, a.aabb);
            }
        }
        else if (a.type == CollisionType.Sphere)
        {
            if (b.type == CollisionType.Cube)
            {
                return HasIntersection(a.sphere, b.aabb);
            }
            else if (b.type == CollisionType.Sphere)
            {
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

    public static void ResolveCollision(PhysicsBehaviour a, PhysicsBehaviour b)
    {
        CollisionManifold manifold = new CollisionManifold(a, b); // The normal will point from a to b

        if (manifold.mPenetration > 0.0f)
        {
            // Linear displacement
            if(a.mobile)
                a.transform.position -= manifold.mNormal.normalized * manifold.mPenetration;
            if(b.mobile)
                b.transform.position += manifold.mNormal.normalized * manifold.mPenetration;

            // Linear Impulse
            Vector3 vr = manifold.mVelocity;
            Vector3 n = manifold.mNormal;
            float vrn = Vector3.Dot(vr, n);
            float e = Mathf.Min(a.bounciness, b.bounciness);
            float InvMasses = (1.0f / a.mass) + (1.0f / b.mass);
            float j = (-(1.0f + e) * vrn) / InvMasses;

            // Friction
            Vector3 t = vr - (vrn * n);
            float jt = (-(1.0f + e) * (Vector3.Dot(vr, t))) / InvMasses;
            float friction = Mathf.Sqrt(a.friction * b.friction);
            jt = Mathf.Max(jt, -j * friction);
            jt = Mathf.Min(jt, j * friction);

            // Adjust velocities
            a.velocity = a.velocity - ((jt / a.mass) * n);
            b.velocity = b.velocity + ((jt / b.mass) * n);
        }
        b.contacts.Remove(a);
    }

    public static void ResolveCollisions(PhysicsBehaviour obj)
    {
        foreach (PhysicsBehaviour collider in obj.contacts)
        {
            ResolveCollision(obj, collider);
        }
        obj.contacts.Clear();
    }
}
