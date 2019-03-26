using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerializeableCollider<T> : SaveableUnityComponent<T> where T : Collider
{

    private bool isTrigger;

    protected override void saveComponent(T component, PersistentGameDataController.SaveType saveType)
    {
        isTrigger = component.isTrigger;
    }

    protected override void restoreComponent(T component)
    {
        component.isTrigger = isTrigger;
    }
    
}
