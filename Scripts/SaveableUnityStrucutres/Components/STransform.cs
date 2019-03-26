using UnityEngine;

public class STransform : SaveableUnityComponent<Transform>
{

    public Serializable3DVector position;
    public Serializable3DVector rotation;
    public Serializable3DVector scale;

    protected override void saveComponent(Transform component, PersistentGameDataController.SaveType saveType)
    {
        this.position = new Serializable3DVector(component.position);
        this.rotation = new Serializable3DVector(component.eulerAngles);
        this.scale = new Serializable3DVector(component.localScale);
    }

    protected override void restoreComponent(Transform transform)
    {
        transform.position = position.convertIntoVector3D();
        transform.localScale = scale.convertIntoVector3D();
        transform.eulerAngles = rotation.convertIntoVector3D();
    }

}
