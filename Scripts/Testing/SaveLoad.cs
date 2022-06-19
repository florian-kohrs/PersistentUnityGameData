﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{

    public static string GameSaveName = "SpackJerrow";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Load();
        }
    }

    public static void Save()
    {
        //if (GameManager.IsPlayerAlive)
        {
            GamePersistence.SaveGame(GameSaveName);
        }
    }

    public static void Load()
    {
        if (GamePersistence.IsValidSavedGame(GameSaveName))
        {
            GamePersistence.LoadGame(GameSaveName);
        }
    }

}
