using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStaticData : MonoBehaviour
{

    [Save]
    public static int i = 10;

    [Save]
    public static SaveableMonoBehaviour m;

    [RuntimeInitializeOnLoadMethod]
    public static void test()
    {
        SaveableGame.AddStaticGameData(typeof(TestStaticData));

        //StaticGameData<TestStaticData>.Instance.i = 4;
    }

}
