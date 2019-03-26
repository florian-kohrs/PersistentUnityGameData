using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableGameObject
{

    bool IsTransferable { get; }

    /// <summary>
    /// sets the next saveable gameobject in the scene hirachy as parent
    /// (returns false if there is none)
    /// </summary>
    /// <returns></returns>
    bool findAndSetParent();

    bool findAndSetParent(out Stack<int> hirachy);

    IRestorableGameObject saveObjectAndPrepareScripts();

    void saveAllBehaviours(PersistentGameDataController.SaveType saveType);

    void addChildren(ISaveableGameObject gameObject, Stack<int> hirachyPath);

    GameObject GetGameObject();

    int getSiblingIndex();
    
}
