using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RefList<T> : List<T>, ITransformObject where T : IAssetRefHolder
{

    public RefList() { }

    public RefList(IEnumerable<T> xs)
    {
        AddRange(xs);
    }

    public object getTransformedValue()
    {
        return null;// new SRefList<IRestoreObject>(this.Select(t => t.GetReferencer()));
    }

 
    [System.Serializable]
    private class SRefList<J> : List<J> where J : IRestoreObject
    {
        
        public SRefList(IEnumerable<J> xs)
        {
            AddRange(xs);
        }

        public object restoreObject()
        {
            return new RefList<T>(this.Select(t => (T)t.restoreObject()));
        }
    }

}
