using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LineSegment
{
    public Vector3 mStart;
    public Vector3 mEnd;

    public LineSegment(Vector3 start, Vector3 end)
    {
        mStart = start;
        mEnd = end;
    }
    public Vector3 PointOnSegment(float t)
    {
        return mStart + (mEnd - mStart) * t;
    }

    public float MinDistSq(Vector3 point)
    {
        // Construct vectors
        Vector3 ab = mEnd - mStart;
        Vector3 ba = -1.0f * ab;
        Vector3 ac = point - mStart;
        Vector3 bc = point - mEnd;

        // Case 1: C projects prior to A
        if(Vector3.Dot(ab, ac) < 0.0f)
        {
            return ac.sqrMagnitude;
        }

        // Case 2: C projects after B
        else if(Vector3.Dot(ba, bc) < 0.0f)
        {
            return bc.sqrMagnitude;
        }

        // Case 3: C projects onto line
        else
        {
            // Compute p
            float scalar = Vector3.Dot(ac, ab) / Vector3.Dot(ab, ab);
            Vector3 p = scalar * ab;

            // Compute length squared of ac - p
            return (ac - p).sqrMagnitude;
        }
    }
}

[System.Serializable]
public struct Plane
{
    public Vector3 mNormal;
    public float md;

    public Plane(Vector3 a, Vector3 b, Vector3 c)
    {
        // Compute vectors from a to b and a to c
        Vector3 ab = b - a;
        Vector3 ac = c - a;

        // Cross product and normalize to get normal
        mNormal = Vector3.Cross(ab, ac);
        mNormal.Normalize();

        // d = -P dot n
        md = -Vector3.Dot(a, mNormal);
    }
}

[System.Serializable]
public struct Sphere
{
    public Vector3 mCentre;
    public float mRadius;

    public Sphere(Vector3 centre, float radius)
    {
        mCentre = centre;
        mRadius = radius;
    }

    public bool ContainsPoint(Vector3 point)
    {
        // Get distance squared between centre and point
        float distSq = (mCentre - point).sqrMagnitude;
        return distSq <= (mRadius * mRadius);
    }
}

[System.Serializable]
public struct AABB
{
    public Vector3 mMax;
    public Vector3 mMin;

    public AABB(Vector3 max, Vector3 min)
    {
        mMax = max;
        mMin = min;
    }

    public float MinDistSq(Vector3 point)
    {
        // computer differences for each axis
        float dx = Mathf.Max(mMin.x - point.x, 0.0f);
        dx = Mathf.Max(dx, point.x - mMax.x);
        float dy = Mathf.Max(mMin.y - point.y, 0.0f);
        dy = Mathf.Max(dy, point.y - mMax.y);
        float dz = Mathf.Max(mMin.z - point.z, 0.0f);
        dz = Mathf.Max(dz, point.z - mMax.z);

        // Distance squared formula
        return dx * dx + dy * dy + dz * dz;
    }
}
