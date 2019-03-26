using UnityEditor;
using UnityEngine;

public class CheckPrefabs : MonoBehaviour
{

    [MenuItem("SaveablePrefabs/Validate all Prefabs")]
    static void checkAllPrefabs()
    {
        PrefabPathFixer.checkUncheckedPrefabsForSaveableEnvirenment();
    }

    [MenuItem("SaveablePrefabs/Validate unchecked Prefabs")]
    static void checkUncheckedPrefabs()
    {
        PrefabPathFixer.checkUncheckedPrefabsForSaveableEnvirenment();
    }

}
