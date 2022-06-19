using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableComponent
{

    IRestorableComponent CreateRestoreableComponent();

    IRestorableComponent SaveComponent(GameObject gameObject, 
        IComponentAssigner assigner, GamePersistence.SaveType saveType);
    
}
