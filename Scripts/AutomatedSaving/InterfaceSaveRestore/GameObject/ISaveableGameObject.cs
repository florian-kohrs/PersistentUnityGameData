using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableGameObject : IComponentAssigner
{

    bool IsTransferable { get; }

    /// <summary>
    /// sets the next saveable gameobject in the scene hirachy as parent
    /// (returns false if there is none)
    /// </summary>
    /// <returns></returns>
    bool FindAndSetParent();

    bool FindAndSetParent(out Stack<int> hirachy);

    void ResetChildNodes();

    IRestorableGameObject SaveObjectAndPrepareScripts();

    void SaveAllBehaviours(GamePersistence.SaveType saveType);

    void AddChildren(ISaveableGameObject gameObject, Stack<int> hirachyPath);

    GameObject GetGameObject();

    int GetSiblingIndex();
    
}
