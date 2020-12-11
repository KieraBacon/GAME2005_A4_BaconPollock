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
            mPenetration = 0;
            mContacts = new Vector3[0];
        }
        else if(a.type == CollisionType.Cube && b.type == CollisionType.Sphere)
        {
            mPenetration = 0;
            mContacts = new Vector3[0];
        }
        else if(a.type == CollisionType.Sphere && b.type == CollisionType.Cube)
        {
            mPenetration = 0;
            mContacts = new Vector3[0];
        }
        else if(a.type == CollisionType.Sphere && b.type == CollisionType.Sphere)
        {
            mPenetration = 0.5f * (mNormal.magnitude - (a.sphere.mRadius + b.sphere.mRadius));

            mContacts = new Vector3[1];
            float h = (0.5f + a.sphere.mRadius * a.sphere.mRadius - b.sphere.mRadius * b.sphere.mRadius) / (2.0f * mNormal.sqrMagnitude);
            mContacts[0] = a.sphere.mCentre + h * (b.sphere.mCentre - a.sphere.mCentre);
        }
        else
        {
            mPenetration = 0;
            mContacts = new Vector3[0];
        }
    }
}
