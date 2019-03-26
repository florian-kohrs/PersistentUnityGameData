using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// will move all prefabs who are currently not in a resource folder into a resource folder
/// </summary>
public class PrefabPathFixer
{

    public const string RESOURCE_FOLDER_NAME = "Resources";

    public const string PREFAB_FOLDER_NAME = "Prefabs";

    public const string ASSET_FOLDER_NAME = "Assets";

    public static readonly string absoluteAssetFolderPath;

    private static string RelativePrefabPath
    {
        get
        {
            return RESOURCE_FOLDER_NAME + "/" + PREFAB_FOLDER_NAME;
        }
    }

    private static string RelativePrefabPathFromAssetFolder { get
        {
            return ASSET_FOLDER_NAME + "/" + RelativePrefabPath;
        }
    }

    static PrefabPathFixer()
    {
        absoluteAssetFolderPath = Application.dataPath.Remove(Application.dataPath.LastIndexOf("/") + 1);
    }

    public static void checkUncheckedPrefabsForSaveableEnvirenment()
    {
        checkPrefabsForSaveableEnvirenment(false);
    }

    public static void checkAllPrefabsForSaveableEnvirenment()
    {
        checkPrefabsForSaveableEnvirenment(true);
    }

    private static void checkPrefabsForSaveableEnvirenment(bool onlyValidateCurrentlyUnchecked = true)
    {
        foreach(string path in getAllPrefabPathes())
        {
            GameObject current = getPrefabFromPath(path);
            SaveablePrefabRoot saveBehaviour = current.GetComponent<SaveablePrefabRoot>();
            if (saveBehaviour != null)
            {
                checkPrefab(path, saveBehaviour);
            }
        }
    }
    
    private static void checkPrefab(string path, SaveablePrefabRoot saveablePrefab)
    {
        string relativePrefabPath = path;

        ///only move the prefab when its not already located in a "Resources" folder
        if (!saveablePrefab.prefabAlreadyValidated)
        {
            string newRelativeAssetPath;

            ///check if the prefab is already in the correct directory
            if (relativePrefabPath.IndexOf(RelativePrefabPath) == -1)
            {
                newRelativeAssetPath = relativePrefabPath.Insert
                (relativePrefabPath.IndexOf("/"), "/" + RelativePrefabPath);
            }
            else
            {
                newRelativeAssetPath = relativePrefabPath;
            }
            string newRelativePrefabDirectory = Path.GetDirectoryName(newRelativeAssetPath);

            string assetMoveErrorMessage = null;

            ///only move the asset when the relevant path part is not already
            ///included in the current path
            if (relativePrefabPath.IndexOf(RelativePrefabPath) == -1)
            {
                FolderSystem.createAssetPath(newRelativePrefabDirectory.Split('/'));
                assetMoveErrorMessage = AssetDatabase.MoveAsset(relativePrefabPath, newRelativeAssetPath);
            }

            if (string.IsNullOrEmpty(assetMoveErrorMessage))
            {
                saveablePrefab.prefabAlreadyValidated = true;
                saveablePrefab.PrefabName = saveablePrefab.gameObject.name;
                string pathFromAssetFolder =
                    Path.GetDirectoryName(relativePrefabPath.Remove(0, RelativePrefabPathFromAssetFolder.Length + 1));
                saveablePrefab.RelativeResourceFolderPath = pathFromAssetFolder;
                var prefabStage = PrefabStageUtility.GetPrefabStage(saveablePrefab.gameObject);
                if (prefabStage != null) { EditorSceneManager.MarkSceneDirty(prefabStage.scene); }
                PrefabUtility.SaveAsPrefabAsset(saveablePrefab.gameObject, newRelativeAssetPath);
                PrefabUtility.UnloadPrefabContents(saveablePrefab.gameObject);
            }
            else
            {
                Debug.LogError("Asset couldnt be moved to path: " + newRelativePrefabDirectory + ". Error: " + assetMoveErrorMessage);
            }
        }
    }

    private static List<string> getAllPrefabPathes()
    {
        List<string> result = new List<string>();
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string s in allAssetPaths)
        {
            if (s.Contains(".prefab"))
            {
                result.Add(s);
            }
        }
        return result;
    }


    /// <summary>
    /// returns the prefab at the given path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static GameObject getPrefabFromPath(string path)
    {
        return (GameObject)PrefabUtility.LoadPrefabContents(path);
    }
    
}
