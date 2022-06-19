using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Transferable]
public class SaveablePrefabChild : BaseSaveableGameObject
{
    protected override IRestorableGameObject CreateSerializableObject()
    {
        return new SerializablePrefabChild(gameObject, transform.GetSiblingIndex());
    }
}
