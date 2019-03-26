using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

public class AutomatedScriptTransfer
{
    
    public static void transferScriptsSaving(object source, Dictionary<string, object> target, PersistentGameDataController.SaveType transferState)
    {
        Type sourceType = source.GetType();
        
        Type savedAttribute = typeof(SaveAttribute);
        
        foreach (FieldInfo f in getFieldsFromType(sourceType, typeof(SaveableMonoBehaviour)))
        {
            if (f.IsDefined(savedAttribute, true))
            {
                SaveAttribute currentSaveAttribute = ((SaveAttribute)
                    (f.GetCustomAttributes(savedAttribute, false)[0]));

                if ((transferState == PersistentGameDataController.SaveType.Game && currentSaveAttribute.saveForScene)
                    || (transferState == PersistentGameDataController.SaveType.Scene && currentSaveAttribute.saveForGame))
                {
                    object value = getValue(f.GetValue(source));
                    target.Add(f.Name, value);
                }
            }
        }
    }

    public static void restoreData(Dictionary<string, object> source,
        ISaveableComponent target, Type lastSerializedType)
    {
        FieldInfo[] fields = getFieldsFromType(target.GetType(), lastSerializedType);

        foreach (KeyValuePair<string,object> entry in source)
        {
            fields.Where(field => field.Name == entry.Key).First().
                SetValue(target, getValue(entry.Value));
        }
    }
    
    private static FieldInfo[] getFieldsFromType(Type target, Type last, bool lastInclusive = true)
    {
        List<FieldInfo> fields = new List<FieldInfo>();

        ///add all public and private fields of current type
        fields.AddRange(target.GetFields((BindingFlags.Public
            | BindingFlags.NonPublic | BindingFlags.Instance)));

        ///add all private fields of parent types. Stops after type "BaseSaveableMonoBehaviour" 
        Type parentType = target;
        while (parentType != last && (lastInclusive || parentType.BaseType != last))
        {
            parentType = parentType.BaseType;
            fields.AddRange(parentType.GetFields((
                BindingFlags.NonPublic | BindingFlags.Instance)));
        }

        return fields.ToArray();
    }
    
    /// <summary>
    /// returns the value of an object, unless its an ITransformObject. Then 
    /// the getTransformedValue will get called and returned.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static object getValue(object source)
    {
        object result = source;

        if (result is ITransformObject)
        {
            ITransformObject transformObject = (ITransformObject)result;
            result = transformObject.getTransformedValue();
        }
        
        return result;
    }

    /// <summary>
    /// stores all fieldValues which are Serializable into the serialization info
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void transferComponentSaving(SaveableComponent source, Dictionary<string, object> target)
    {
        FieldInfo[] targetFields = getFieldsFromSaveableComponent(source.GetType());

        foreach(FieldInfo f in targetFields)
        {
            target.Add(f.Name, getValue(f.GetValue(source)));
        }

    }

    /// <summary>
    /// returns all field infos from the given type until the SerializableUnityComponent<?> type
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private static FieldInfo[] getFieldsFromSaveableComponent(Type target)
    {
        List<FieldInfo> fields = new List<FieldInfo>();

        ///add all public and private fields of current type
        fields.AddRange(target.GetFields((BindingFlags.Public
            | BindingFlags.NonPublic | BindingFlags.Instance)));

        ///add all private fields of parent types. Stops after type "BaseSaveableMonoBehaviour" 
        Type parentType = target;
        while (parentType.BaseType.BaseType != typeof(SaveableComponent))
        {
            parentType = parentType.BaseType;
            fields.AddRange(parentType.GetFields((
                BindingFlags.NonPublic | BindingFlags.Instance)));
        }

        return fields.ToArray();
    }

}
