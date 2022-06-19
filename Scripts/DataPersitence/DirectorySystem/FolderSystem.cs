using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FolderSystem {

    #region store settings
    ///in this region, the structur of the stored data is set.
    ///current try: each scene is safed in an own file. a gameslot is defined by an order
    ///containing multiple files of different scenes
    ///this is done to minimize the loading time by loading few small files
    ///instead if one big, as its complexity is O^2
    public static readonly string saveDirName = "Stored";

    public static readonly string sceneFolderName = "Scenes";
    
    public static readonly string settingsFileName = "settings";

    public static readonly string fileType = "Bodo";

    #endregion

    /// <summary>
    /// creates the given relative path as standart directory.
    /// </summary>
    /// <param name="path">path</param>
    /// <param name="startHere">start path that is guaranteed to exist</param>
    public static void CreatePath(params string[] pathParts)
    {
        string currentPath = Combine(pathParts);

        if (!Directory.Exists(currentPath))
        {
            Directory.CreateDirectory(currentPath);
        }
    }

    /// <summary>
    /// creates the given relative path as asset directory.
    /// </summary>
    /// <param name="pathParts"></param>
    public static void CreateAssetPath(params string[] pathParts)
    {
#if UNITY_EDITOR
        if (pathParts != null && pathParts.Length > 0)
        {
            string relativeBuildPath = pathParts[0];
            Array.ForEach(
                pathParts.Skip(1).ToArray(),
                (path) =>
                {
                    string newPath = relativeBuildPath + "/" + path;
                    if (!AssetDatabase.IsValidFolder(newPath))
                    {
                        AssetDatabase.CreateFolder(relativeBuildPath, path);
                    }
                    relativeBuildPath = newPath;
                }
               );
        }
#endif
    }

    /// <summary>
    /// combines the pathParts by putting an '/' between each string if the string doesnt already end with an '/'
    /// </summary>
    /// <param name="pathParts"></param>
    /// <returns></returns>
    public static string Combine(params string[] pathParts)
    {
        string result = "";
        Array.ForEach(pathParts, (p) => {
            if (result.Length > 0 && result[result.Length - 1] != '/')
            {
                result += "/";
            }
            result += p;
        });
        return result;
    }
    
    public static string[] GetAllSaveSlotNames()
    {
        return Directory.GetDirectories(GetDefaultSaveSlotPath());
    }

    public static void CreateDefaultFolderSystem()
    {
        CreatePath(Application.persistentDataPath, saveDirName);
    }

    public static void DeleteGame(SaveableGame game)
    {
        string path = GetGameDirectory(game.GameName);
        Directory.Delete(path, true);
    }

    /// <summary>
    /// creates the foldersystem for the given game name
    /// </summary>
    /// <param name="gameName"></param>
    /// <returns>return the full path for the scene folder</returns>
    public static string CreateNewSaveSlotDirectory(string gameName)
    {
        string defaultPath = GetDefaultSaveSlotPath();
        string path = GetDefaulScenePath(gameName);
        CreatePath(defaultPath, gameName, sceneFolderName);
        return path;
    }

    public static string GetGameDirectory(string gameName)
    {
        return GetDefaultSaveSlotPath() + "/" + gameName;
    }

    public static string GetDefaulScenePath(string gameName)
    {
        return GetDefaultSaveSlotPath() + "/" + gameName + "/" + sceneFolderName;
    }

    public static string GetSettingsPath()
    {
        return GetDefaultSaveSlotPath() + "/" + settingsFileName + "." + settingsFileName;
    }

    public static string GetDefaultSettingsPath()
    {
        return GetDefaultSaveSlotPath();
    }

    public static string GetDefaultSaveSlotPath()
    {
        return Application.persistentDataPath + "/" + saveDirName;
    }

    public static string GetGameSavePath(SaveableGame game)
    {
        return GetGameSavePath(game.GameName);
    }

    public static string GetGameSavePath(string gameName)
    {
        string result = GetDefaultSaveSlotPath() + "/" + gameName + "/" + 
            gameName+ "." + fileType;
        return result;
    }
    
    public static bool SceneExists(string gameSlotName, string sceneName)
    {
        return File.Exists(GetSceneSavePath(gameSlotName, sceneName));
    }
    
    public static string GetSceneSavePath(SaveableGame game, string sceneName)
    {
        return GetSceneSavePath(game.GameName, sceneName);
    }

    public static string GetSceneSavePath(string gameName, string sceneName)
    {
        string result = Application.persistentDataPath + "/" +
            saveDirName + "/" + gameName + "/" + sceneFolderName + "/" +
            sceneName + "." + fileType;
        return result;
    }

}
