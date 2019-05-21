using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class AssetReference : IAssetReferencer, IRestoreObject
{

    public System.Type assetType;

    [SerializeField]
    private string relativePathFromResource;

    public string RelativePathFromResource
    {
        get { return relativePathFromResource; } set { relativePathFromResource = value; }
    }

    [SerializeField]
    private string assetName;

    public string AssetName
    {
        get { return assetName; }
        set { assetName = value; }
    }

    [SerializeField]
    private string assetExtension;

    public string AssetExtension
    {
        get { return assetExtension; }
        set { assetExtension = value; }
    }

    [SerializeField]
    private bool wasAlreadyValidated;

    public bool WasAlreadyValidated
    {
        get { return wasAlreadyValidated; }
        set { wasAlreadyValidated = value; }
    }

    public object restoreObject()
    {
        Object result;
        if(!refRestoreTable.TryGetValue(this, out result))
        {
            result = Resources.Load(RelativePathFromResource + AssetName, assetType);
            refRestoreTable.Add(this, result);
        }
        return result;
    }
    
    /// <summary>
    /// this dictionary will prevent the same asset references from 
    /// being searched in the resource folder multiple times
    /// </summary>
    private static Dictionary<AssetReference, Object> refRestoreTable = 
        new Dictionary<AssetReference, Object>();
    
}
