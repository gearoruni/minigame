using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private int id = 0;
    private const string UIPath = "Assets/UI";
    private Transform canvas;
    private Dictionary<int, GameObject> _idx2UI = new Dictionary<int, GameObject>();
    public override void Init()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    public int ShowUI(string name)
    {
        var go = AssetDatabase.LoadAssetAtPath<GameObject>(UIPath + name);
        if(go == null)
        {
            Debug.LogError($"º”‘ÿUI ß∞‹£¨name£∫{name}");
            return -1;
        }
        go.transform.SetParent(canvas);
        _idx2UI.Add(id, go);
        return id++;
    }

    public void CloseUI(int id)
    {
        if(_idx2UI.TryGetValue(id, out var UI))
        {
            GameObject.Destroy(UI);
            _idx2UI.Remove(id);
        }
    }
}
