using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class Preloader : MonoSingleton<Preloader>
{
    private const string ASSET_PATH = "Assets/Res/Preload";
    private Dictionary<string, GameObject> preloadItem = new Dictionary<string, GameObject>();

    public void Init()
    {
        string[] filedir = Directory.GetFiles(ASSET_PATH, "*", SearchOption.AllDirectories);
        string name;
        for (int i = 0; i < filedir.Length; i++)
        {
            name = Regex.Match(filedir[i], @"([^\\]*)\.prefab$").Groups[1].Value;

            if (name == "") continue;

            var go = AssetDatabase.LoadAssetAtPath<GameObject>(filedir[i]);
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