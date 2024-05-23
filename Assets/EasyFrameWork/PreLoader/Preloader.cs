using Pathfinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using UnityEngine;

public class Preloader : MonoSingleton<Preloader>
{
    //private const string ASSET_PATH = "Assets/Res/Preload";
    private static string ASSET_PATH =  Application.dataPath + "/Resources/Preload";

    private Dictionary<string, GameObject> preloadItem = new Dictionary<string, GameObject>();

    public void Init()
    {
        TextAsset itemList = Resources.Load<TextAsset>("Preload/preloader");
        
        string[] items = itemList.text.Split('\n');
        string name;

        foreach (string item in items)
        {
            string itemt = item.Split("\r")[0];
            string[] temp = itemt.Split("/");
            name = temp[temp.Length - 1];
            var go = Resources.Load<GameObject>(itemt);
            
            preloadItem.Add(name, go);
        }
    }

    public GameObject GetGameObject(string name)
    {
        if (preloadItem.TryGetValue(name, out var gameObject))
        {
            return gameObject;
        }
        return null;
    }
}