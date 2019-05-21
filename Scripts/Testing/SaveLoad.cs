using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        { 
            PersistentGameDataController.SaveGame("a");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PersistentGameDataController.LoadGame("a");
        }
    }
}
