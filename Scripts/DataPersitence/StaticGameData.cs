using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this class is only experimental and will probalby not proove usefull, thus will be removed in the future
/// baseclass for all classes with object independent data - static data
/// </summary>
/// <typeparam name="T">T should be type of derived class</typeparam>
public class StaticGameData<T> where T : class, new()
{

    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    static StaticGameData()
    {
        SaveableGame.AddStaticGameData(typeof(T));
    }

}

