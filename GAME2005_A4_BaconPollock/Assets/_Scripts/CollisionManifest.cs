using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct CollisionManifold
{
    public enum Nature
    {
        NoCollision,
        Colliding,
        Penetrating
    }
    public Nature mNature;
    public PhysicsBehaviour mA;
    public PhysicsBehaviour mB;
    public Vector3 mVelocity;
    public Vector3 mNormal;
    public float mPenetration;
    public Vector3[] mContacts;

    public CollisionManifold(PhysicsBehaviour a, PhysicsBehaviour b)
    {
        mNature = Nature.Colliding;
        mA = a;
        mB = b;
        mVelocity = b.velocity - a.velocity;
        mNormal = b.transform.position - a.transform.position;

        if (a.type == CollisionType.Cube && b.type == CollisionType.Cube)
        {

            mPenetration = 0.0f;
            mContacts = new Vector3[1];

            Vector3 overlap = new Vector3(Mathf.Min((b.aabb.mMax.x - a.aabb.mMin.x), (a.aabb.mMax.x - b.aabb.mMin.x)),
                                          Mathf.Min((b.aabb.mMax.y - a.aabb.mMin.y), (a.aabb.mMax.y - b.aabb.mMin.y)),
                                          Mathf.Min((b.aabb.mMax.z - a.aabb.mMin.z), (a.aabb.mMax.z - b.aabb.mMin.z)));

            float minOverlap = Mathf.Min(overlap.x, overlap.y, overlap.z);
            if (minOverlap == overlap.x)
            {
                if ((b.aabb.mMax.x - a.aabb.mMin.x) < (a.aabb.mMax.x - b.aabb.mMin.x)) mNormal = new Vector3(-overlap.x, 0.0f, 0.0f);
                else mNormal = new Vector3(overlap.x, 0.0f, 0.0f);
                mPenetration = overlap.x * 0.5f;
            }
            if (minOverlap == overlap.y)
            {
                if ((b.aabb.mMax.y - a.aabb.mMin.y) < (a.aabb.mMax.y - b.aabb.mMin.y)) mNormal = new Vector3(0.0f, -overlap.y, 0.0f);
                else mNormal = new Vector3(0.0f, overlap.y, 0.0f);
                mPenetration = overlap.y * 0.5f;
            }
            if (minOverlap == overlap.z)
            {
                if ((b.aabb.mMax.z - a.aabb.mMin.z) < (a.aabb.mMax.z - b.aabb.mMin.z)) mNormal = new Vector3(0.0f, 0.0f, -overlap.z);
                else mNormal = new Vector3(0.0f, 0.0f, overlap.z);
                mPenetration = overlap.z * 0.5f;
            }
                //mPenetration = 0.001f;
                //mContacts = new Vector3[1];
                //Debug.Log("CUBE/CUBE COLLISION");

                //mPenetration = a.aabb.MinDistSq(b.aabb.mMax);
                //// mPenetration = -0.5f * (mNormal.magnitude - (a.aabb.MinDistSq((b.aabb.mMin) - (b.aabb.mMax)) + b.aabb.MinDistSq((a.aabb.mMin) - (a.aabb.mMax))));
                //Debug.Log("CUBE/CUBE mPenetration =" + mPenetration);
                //mContacts = new Vector3[1];
                ////float h = (0.5f + a.sphere.mRadius * a.sphere.mRadius - b.aabb.MinDistSq(a.sphere.mCentre) * b.aabb.MinDistSq(a.sphere.mCentre)) / (2.0f * mNormal.sqrMagnitude);
                //// contact point is halway between the objects along the collision normal
                //mContacts[0] = Vector3.Scale(mNormal, a.aabb.mMax);
                ////mContacts[0] = (0.5f * h) * mNormal; // Hmmm..
                ////Debug.Log(mContacts[0]);

            }
        else if(a.type == CollisionType.Cube && b.type == CollisionType.Sphere)
        {
            mPenetration = 0;
            mContacts = new Vector3[0];
            Debug.Log("CUBE/SPHERE COLLISION");
            mContacts[0] = a.aabb.mMax;


            // mPenetration = a.aabb.MinDistSq(b.aabb.mMax);
            //// mPenetration = -0.5f * (mNormal.magnitude - (a.aabb.MinDistSq((b.aabb.mMin) - (b.aabb.mMax)) + b.aabb.MinDistSq((a.aabb.mMin) - (a.aabb.mMax))));
            // Debug.Log("CUBE/CUBE mPenetration =" + mPenetration);
            // mContacts = new Vector3[1];
            // //float h = (0.5f + a.sphere.mRadius * a.sphere.mRadius - b.aabb.MinDistSq(a.sphere.mCentre) * b.aabb.MinDistSq(a.sphere.mCentre)) / (2.0f * mNormal.sqrMagnitude);
            // // contact point is halway between the objects along the collision normal
            // mContacts[0] = Vector3.Scale(mNormal, a.aabb.mMax);
            // //mContacts[0] = (0.5f * h) * mNormal; // Hmmm..
            // //Debug.Log(mContacts[0]);


            //// The penetration depth = 0.5f * (closest point on sphere - closest point on cube) 
            //mPenetration = -0.5f * (mNormal.magnitude - (b.sphere.mRadius + a.aabb.MinDistSq(b.sphere.mCentre)));
            //Debug.Log("mPenetration =" + mPenetration);
            //mContacts = new Vector3[1];
            //float h = (0.5f + b.sphere.mRadius * b.sphere.mRadius - a.aabb.MinDistSq(b.sphere.mCentre) * a.aabb.MinDistSq(b.sphere.mCentre)) / (2.0f * mNormal.sqrMagnitude);
            //// contact point is halway between the objects along the collision normal
            //mContacts[0] = Vector3.Scale(mNormal, (b.sphere.mRadius * b.sphere.mCentre));
            ////mContacts[0] = (0.5f * h) * mNormal; // Hmmm..
            ////Debug.Log(mContacts[0]);

        }
        else if(a.type == CollisionType.Sphere && b.type == CollisionType.Cube)
        {
            //mPenetration = 0;
            //mContacts = new Vector3[0];

            // The penetration depth = 0.5f * (closest point on sphere - closest point on cube) 
            //mPenetration = 0.001f;
            mPenetration = 0.5f * (mNormal.magnitude - (a.sphere.mRadius + b.aabb.MinDistSq(a.sphere.mCentre)));
            Debug.Log("mPenetration =" + mPenetration);
            mContacts = new Vector3[1];
            float h = (0.5f + a.sphere.mRadius * a.sphere.mRadius - b.aabb.MinDistSq(a.sphere.mCentre) * b.aabb.MinDistSq(a.sphere.mCentre)) / (2.0f * mNormal.sqrMagnitude);
            // contact point is halway between the objects along the collision normal
            mContacts[0] = Vector3.Scale(mNormal, (a.sphere.mRadius * a.sphere.mCentre));
            //mContacts[0] = (0.5f * h) * mNormal; // Hmmm..
            //Debug.Log(mContacts[0]);
        }
        else if(a.type == CollisionType.Sphere && b.type == CollisionType.Sphere)
        {
            mPenetration = -0.5f * (mNormal.magnitude - (a.sphere.mRadius + b.sphere.mRadius));
            //Debug.Log(mPenetration);
            mContacts = new Vector3[1];
            float h = (0.5f + a.sphere.mRadius * a.sphere.mRadius - b.sphere.mRadius * b.sphere.mRadius) / (2.0f * mNormal.sqrMagnitude);
            mContacts[0] = a.sphere.mCentre + h * (b.sphere.mCentre - a.sphere.mCentre);
            //Debug.Log(mContacts[0]);
            //Debug.Log("SPHERE/SPHERE COLLISION");
        }
        else
        {
            mPenetration = 0;
            mContacts = new Vector3[0];
        }
    }
}
