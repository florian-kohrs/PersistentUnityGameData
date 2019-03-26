using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Serializable3DVector
{

    float x;
    float y;
    float z;

    public Serializable3DVector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Serializable3DVector(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 convertIntoVector3D()
    {
        return new Vector3(x, y, z);
    }

    public static Serializable3DVector convertIntoSaveableVector(Vector3 vector)
    {
        return new Serializable3DVector(vector);
    }

}
