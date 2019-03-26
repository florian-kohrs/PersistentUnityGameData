using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapScenes : MonoBehaviour
{

    [SerializeField]
    public SaveablePrefabRoot saveableObject;

    static bool dir;
    static int count = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (dir)
            {
                SceneSwitcher.enterScene("SmallTestScene", true, true);
            }
            else
            {
                if (count == 0)
                {
                    SceneSwitcher.enterScene("EmptyScene", true, true, saveableObject);
                }
                else
                {
                    SceneSwitcher.enterScene("EmptyScene", true, true);
                }
               
            }
            dir = !dir;
            count++;
        }
    }
}
