using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableScriptableObject : ScriptableObject, ITransformObject
{

    [HideInInspector]
    public AssetReference assetRef;

    public object getTransformedValue()
    {
        return assetRef;
    }
}
