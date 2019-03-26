using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Transferable]
public class SaveablePrefabRoot : BaseSaveableGameObject
{
    
    [SerializeField, Tooltip("The name of the corresponding prefab.")]
    private string prefabName;

    [SerializeField, Tooltip("The default path is \"Resources/Prefabs\". If the prefab is in another " +
        "Folder within the \"Prefabs\" folder, set the relative path here.")]
    private string relativeResourceFolderPath;
    
    public string RelativeResourceFolderPath
    {
        get { return relativeResourceFolderPath; }
        set { relativeResourceFolderPath = value; }
    }

    public string PrefabName { get { return prefabName; } set { prefabName = value; } }

    public string Prefabpath { get { return relativeResourceFolderPath; } set { relativeResourceFolderPath = value; } }

    public bool prefabAlreadyValidated;
    
    protected override void Awake()
    {
        Debug.Log("Prefab Root awakend");
        base.Awake();
        if (!SaveableGame.KeepObjects)
        {
            ///if the object is not gonna be kept, it is added to the garbage heap so it can be deleted later
            SaveableGame.addObjectToGarbageHeap(gameObject);
        }
        Debug.Log("Awake done");

    }

    public new void prepareSceneTransition()
    {
        base.prepareSceneTransition();
    }

    protected override IRestorableGameObject createSerializableObject()
    {
        return new SerializablePrefabRoot(gameObject, PrefabName, relativeResourceFolderPath);
    }
}
