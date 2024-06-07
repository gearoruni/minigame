using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private int id = 0;
    private const string UIPath = "Assets/UI//.prefab";
    private Transform canvas;
    private Dictionary<int, GameObject> _idx2UI = new Dictionary<int, GameObject>();
    public override void Init()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    public int ShowUI(string name)
    {
        var go = Resources.Load<GameObject>("UI/" + name);
        //var go = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/UI/{name}.prefab");
        if(go == null)
        {
            Debug.LogError($"º”‘ÿUI ß∞‹£¨name£∫{name}");
            return -1;
        }
        go = GameObject.Instantiate(go);
        go.transform.SetParent(canvas, false);
        /*var rect = go.GetComponent<RectTransform>();
        rect.SetParent(canvas);*/
        // var rect = go.GetComponent<RectTransform>();
        // rect.SetParent(canvas);
        
        // go.transform.localPosition = Vector3.zero;
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
