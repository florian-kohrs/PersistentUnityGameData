using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSaveableMonoBehaviour : BaseSaveableObject, ITransformObject
{

    /// <summary>
    /// this bool indicates that the script was in the saved component list, thus it wont be destroyed.
    /// All not loaded saveable scripts, which were attached to the created prefab will be destroyed.
    /// </summary>
    public bool WasCreated { get; set; }
    
    public object GetTransformedValue()
    {
        return CreatedSaveableMonoBehaviour;
    }

    public abstract void SetSaveData(GamePersistence.SaveType saveType);
    
    public abstract void OnBehaviourLoaded();
    
    /// <summary>
    /// onAwake is called when the normal awake is called, during loading phase
    /// awake is not called, as the loading initialsation isnt finished at this time
    /// use "behaaviourLoaded" instead!
    /// </summary>
    public virtual void OnAwake() { }

    public IRestorableComponent CreatedSaveableMonoBehaviour { get; private set; }

    private Dictionary<string, object> savedDictionary;

    public override IRestorableComponent CreateRestoreableComponent()
    {
        IRestorableComponentContainer savedScript = 
            new SaveableMonoBehaviourContainer(GetType());

        IRestorableComponent result = savedScript;

        savedDictionary = savedScript.DataContainer;

        CreatedSaveableMonoBehaviour = result;

        return result;
    }

    public override IRestorableComponent SaveComponent(GameObject gameObject, 
        IComponentAssigner assigner, GamePersistence.SaveType saveType)
    {
        SetSaveData(saveType);
        AutomatedScriptTransfer.TransferScriptsSaving(this, savedDictionary, saveType);
        return CreatedSaveableMonoBehaviour;
    }

}
