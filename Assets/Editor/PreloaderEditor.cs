using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PreloaderEditor :Editor
{
    private static string ASSET_PATH = Application.dataPath + "/Resources/Preload";

    private Dictionary<string, GameObject> preloadItem = new Dictionary<string, GameObject>();

    [MenuItem("Preloader/CreatePreloaderList")]
    public static void CreateFileList()
    {
        List<string> infos = new List<string>();
        string[] filedir = Directory.GetFiles(ASSET_PATH, "*", SearchOption.AllDirectories);
        string name;
        for (int i = 0; i < filedir.Length; i++)
        {
            name = Regex.Match(filedir[i], @"([^\\]*)\.prefab$").Groups[1].Value;
            string path = PathUtils.GetResPreloadPath(filedir[i]);
            if (Path.GetExtension(path) == ".meta") continue;
            path = PathUtils.GetStanderdPath(Path.GetDirectoryName(path));
            if (name == "") continue;

            string info = path + "/" + name;
            infos.Add(info);
        }
        File.WriteAllLines(ASSET_PATH + "/preloader.txt", infos);
        
        Debug.Log("生成文件 : [res] :" + File.Exists(ASSET_PATH + "/preloader.txt"));
    }
}
