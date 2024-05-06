using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ·������
/// </summary>
public static class PathUtils
{
    // ·�� �� Assets/
    public static readonly string AssetPath = Application.dataPath + "/";

    // Lua�ű�·��
    public static readonly string LuaPath = "Assets/Scripts/LuaScripts/";


    //prefab·��
    public static readonly string prefabPath = "Assets/Prefabs/";

    //�ӵ�����·��
    public static readonly string bulletConfigPath = "Assets/Configs/BulletConfigs/";

    /// <summary>
    /// ԭ���ļ���·�����Unity��׼�ļ�·��
    /// </summary>
    /// <param name="path">·��</param>
    /// <returns></returns>
    public static string GetStanderdPath(string path)
    {
        if(string.IsNullOrEmpty(path))return string.Empty;

        return path.Trim().Replace("\\", "/");
    }
    public static string GetRequirePath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;

        return path.Trim().Replace("/", ".");
    }
    /// <summary>
    /// ���Assets�µ����·��
    /// </summary>
    /// <param name="path">·��</param>
    /// <returns></returns>
    public static string GetUntiyPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;
        return path.Trim().Substring(path.IndexOf("Assets"));
    }

    public static string GetLuaPath(string path)
    {
        path = GetStanderdPath((string)path);
        path = GetUntiyPath((string)path);
        return path.Replace(".lua", "");
    }

    /// <summary>
    /// ͨ��id��ȡ��Ӧid��BulletConfig
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetBulletConfigsFromId(int id)
    {
        string path = bulletConfigPath + id.ToString() + "Config.asset";

        return path;
    }

    /// <summary>
    /// ͨ��id��ȡ��Ӧid�Ľ�ɫPrefab
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetCharacterPrefabFromID(int id)
    {
        string path = prefabPath + "Characters/" + id.ToString() + ".prefab";
        return path;
    }

    /// <summary>
    /// ͨ��id��ȡ��Ӧid������Prefab
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetWeaponPrefabFromID(int id)
    {
        string path = prefabPath + "Weapons/" + id.ToString() + ".prefab";
        return path;
    }

    /// <summary>
    /// ͨ��id��ȡ��Ӧid���ӵ�Prefab
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetBulletPrefabFromID(int id)
    {
        string path = prefabPath + "Bullets/" + id.ToString() + ".prefab";
        return path;
    }
}