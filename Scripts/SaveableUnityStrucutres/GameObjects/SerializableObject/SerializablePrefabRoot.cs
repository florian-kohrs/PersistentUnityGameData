using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SerializablePrefabRoot : BaseCorrespondingSerializableGameObject
{
    public SerializablePrefabRoot
        (GameObject gameObject, string prefabName, string prefabPath = "") : base(gameObject)
    {
        this.prefabName = prefabName;
        this.prefabPath = prefabPath;
    }

    private readonly string prefabName;

    private readonly string prefabPath;
    
    protected override GameObject getSceneGameObject(Transform parent)
    {
        return instantiateSaveableGameObject(getPrefabFromName(prefabName, prefabPath));
    }

    /// <summary>
    /// calls the default instantiate method 
    /// (Could be left out -> may be done in the future)
    ///</summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    private GameObject instantiateSaveableGameObject(GameObject gameObject)
    {
        GameObject result = GameObject.Instantiate(gameObject);
        return result;
    }

    /// <summary>
    /// returns the prefab in the resource folder with the given name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private GameObject getPrefabFromName(string name, string prefabPath = "")
    {
        if (prefabPath != "" && prefabPath[prefabPath.Length - 1] != '/')
        {
            prefabPath += "/";
        }

        GameObject result = Resources.Load<GameObject>("Prefabs/" + prefabPath + name);

        return result;
    }

}
