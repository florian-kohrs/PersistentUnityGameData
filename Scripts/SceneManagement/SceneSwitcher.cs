using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher
{

    public SceneSwitcher(PersistentGameDataController gameDataController)
    {
        this.gameDataController = gameDataController;
    }

    private PersistentGameDataController gameDataController;

    private static SceneSwitcher instance;

    private static SceneSwitcher getInstance()
    {
        if (instance == null)
        {
            instance = PersistentGameDataController.PrepareSceneSwitcher();
        }
        return instance;
    }

    /// <summary>
    /// saves the current scene and loads the new one afterwards. The parameters
    /// "saveCurrent" and "loadNext" can be used to not save the current scene,
    /// and not loading the next scene. The intention for this is, that the menue
    /// probalby should not be saved
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="saveCurrent">should the current scene be saved?</param>
    /// <param name="loadNext">should the next scene be loaded?</param>
    /// <param name="transferObjects">theese objects will be taken to the next scene</param>
    public static void enterScene(string sceneName, bool saveCurrent = true, 
        bool loadNext = true, params ISaveableGameObject[] transferObjects)
    {
        getInstance().gameDataController.enterScene(sceneName, 
            saveCurrent, loadNext, transferObjects);
    }
    

    /// <summary>
    /// enters the new scene while saving the current and loading the next one
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="transferObjects"></param>
    public static void enterScene(string sceneName, 
        params ISaveableGameObject[] transferObjects)
    {
        getInstance().gameDataController.enterScene(sceneName, transferObjects);
    }
}
